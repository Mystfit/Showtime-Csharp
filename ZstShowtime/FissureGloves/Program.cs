using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using ZST;
using Uniduino;
using MathGeom;
using Newtonsoft.Json;

namespace FissureGloves
{
    class Program
    {
        static void Main(string[] args)
        {
            //Setup Hydra
            SixenseInput input = new SixenseInput();
            input.Init();

            Thread.Sleep(2000);

            input.RebindHands();
            Console.WriteLine("Hydra activated");

            //Setup firmata
            Arduino[] gloves = new Arduino[2];
            int[] bendPins = { 1, 0, 3, 2 };
            int[] bendValues = new int[bendPins.Length];

            try
            {
                gloves[0] = new Arduino("COM6", 57600, true, 0);
                gloves[1] = new Arduino("COM7", 57600, true, 0);

                for (int i = 0; i < bendPins.Length; i++)
                {
                    gloves[0].pinMode(bendPins[i], PinMode.ANALOG);
                    gloves[0].reportAnalog(bendPins[i], 1);
                    gloves[1].pinMode(bendPins[i], PinMode.ANALOG);
                    gloves[1].reportAnalog(bendPins[i], 1);
                }
            }
            catch (System.IO.IOException e)
            {
                Console.WriteLine(e);
            }
            Console.WriteLine("Gloves activated");

            //Setup Showtime
            ZstNode node = new ZstNode("FissureGloves", "tcp://curiosity.soad.vuw.ac.nz:6000");
            node.requestRegisterNode();
            node.requestRegisterMethod("glove_update", ZstMethod.READ);

            Console.WriteLine("Showtime activated");
            
            while (true)
            {
                input.Update();
                if (gloves[0].IsOpen && gloves[1].IsOpen)
                {
                    gloves[0].processInput();
                    gloves[1].processInput();
                }
                

                ZstMethod transformUpdate = node.methods["glove_update"].clone();
                Dictionary<int, object> gloveData = new Dictionary<int, object>();

                for (int i = 0; i < 2; i++)
                {
                   

                    SixenseHands hand = (SixenseHands)i+1;
                    
                    int state = 0;
                    SixensePlugin.sixenseSetHemisphereTrackingMode(i, 1);
                    SixensePlugin.sixenseGetHemisphereTrackingMode(i, ref state);
                    Console.WriteLine(state);
                    Console.WriteLine(SixensePlugin.sixenseGetNumActiveControllers());

                    Vector3 pos = SixenseInput.GetController(hand).Position;
                    Quaternion rot = SixenseInput.GetController(hand).Rotation;

                    if(gloves[0] != null && gloves[1] != null){
                        for (int j = 0; j < bendPins.Length; j++)
                        {
                            bendValues[j] = gloves[i].analogRead(bendPins[j]);
                            //Console.Write(bendValues[j].ToString() + ",");
                        }
                        //Console.WriteLine("");
                    }

                    gloveData[i] = new float[] { 
                        pos.x, 
                        pos.y, 
                        pos.z, 
                        rot.x, 
                        rot.y, 
                        rot.z, 
                        rot.w, 
                        bendValues[0],
                        bendValues[1], 
                        bendValues[2], 
                        bendValues[3],
                    };
                    node.updateLocalMethod(transformUpdate, gloveData);
                }
                Thread.Sleep(10);
            }
        }
    }
}
