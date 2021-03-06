﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NetMQ;
using Newtonsoft.Json;

namespace ZST
{
    public static class ZstIo
    {
        // Remote send/recieve methods
        // ---------------------------
        /// <summary>Send a message to a remote node</summary>
        public static void send(NetMQSocket socket, string method)
        {
            send(socket, method, null);
        }

        /// <summary>Send a message to a remote node using method info</summary>
        public static void send(NetMQSocket socket, string method, ZstMethod methodData)
        {
            NetMQMessage message = new NetMQMessage();
            message.Append(method);
            if (methodData != null)
            {
                string data = JsonConvert.SerializeObject(methodData.as_dict());
                JsonConvert.SerializeObject(data, Formatting.Indented);
                message.Append(data);
            }
            else
            {
                message.Append("{}");
            }

            TimeSpan timeout = TimeSpan.FromSeconds(2);
            socket.SendMessage(message, false);
        }

        /// <summary>Receive a message from a remote node</summary>
        public static MethodMessage recv(NetMQSocket socket)
        {
            return recv(socket, false);
        }

        /// <summary>Receive a message from a remote node</summary>
        public static MethodMessage recv(NetMQSocket socket, bool dontWait)
        {
            try
            {
                TimeSpan timeout = TimeSpan.FromSeconds(5);
                NetMQMessage message = null;
                if(!socket.TryReceiveMultipartMessage(timeout, ref message)) {
                    throw new Exception("Timed out receiving message");
                }
                string method = message[0].ConvertToString();

                //Dictionary<string, object> data = new Dictionary<string, object>();
                string jsonStr = (message[1].ConvertToString());
                Dictionary<string, object> data = JsonConvert.DeserializeObject<Dictionary<string, object>>(jsonStr);

                return new MethodMessage(method, ZstMethod.dictToZstMethod(data));
            }
            catch (NetMQException e)
            {
                Console.Write(e);
            }
            return new MethodMessage("", null);
        }
    }
}
