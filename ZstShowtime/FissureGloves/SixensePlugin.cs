
//
// Copyright (C) 2013 Sixense Entertainment Inc.
// All Rights Reserved
//
// Sixense Driver Unity Plugin
// Version 1.0
//

using System.Collections;
using System.Runtime.InteropServices;
	
/// <summary>
/// The SixensePlugin class provides direct access to the C sixense.dll driver.
/// </summary>
/// <code>
/// #ifndef _SIXENSE_H_
/// #define _SIXENSE_H_
/// 
/// #if defined(WIN32)
///   #ifdef SIXENSE_STATIC_LIB
///     #define SIXENSE_EXPORT 
///   #else
///     #ifdef SIXENSE_BUILDING_DLL
///       #define SIXENSE_EXPORT __declspec(dllexport)
///     #else
///       #define SIXENSE_EXPORT __declspec(dllimport)
///     #endif
///   #endif
/// #else
///   #define SIXENSE_EXPORT 
/// #endif
/// 
/// #define SIXENSE_BUTTON_BUMPER   (0x01<<7)
/// #define SIXENSE_BUTTON_JOYSTICK (0x01<<8)
/// #define SIXENSE_BUTTON_1        (0x01<<5)
/// #define SIXENSE_BUTTON_2        (0x01<<6)
/// #define SIXENSE_BUTTON_3        (0x01<<3)
/// #define SIXENSE_BUTTON_4        (0x01<<4)
/// #define SIXENSE_BUTTON_START    (0x01<<0)
/// 
/// #define SIXENSE_SUCCESS 0
/// #define SIXENSE_FAILURE -1
/// 
/// #define SIXENSE_MAX_CONTROLLERS 4
/// 
/// typedef struct _sixenseControllerData {
///   float pos[3];
///   float rot_mat[3][3];
///   float joystick_x;
///   float joystick_y;
///   float trigger;
///   unsigned int buttons;
///   unsigned char sequence_number;
///   float rot_quat[4];
///   unsigned short firmware_revision;
///   unsigned short hardware_revision;
///   unsigned short packet_type;
///   unsigned short magnetic_frequency;
///   int enabled;
///   int controller_index;
///   unsigned char is_docked;
///   unsigned char which_hand;
///   unsigned char hemi_tracking_enabled;
/// } sixenseControllerData;
/// 
/// typedef struct _sixenseAllControllerData {
///   sixenseControllerData controllers[4];
/// } sixenseAllControllerData;
/// 
/// #if defined(__LANGUAGE_C_PLUS_PLUS)||defined(__cplusplus)||defined(c_plusplus)
/// extern "C" {
/// #endif
/// 
/// SIXENSE_EXPORT int sixenseInit( void );
/// SIXENSE_EXPORT int sixenseExit( void );
/// 
/// SIXENSE_EXPORT int sixenseGetMaxBases();
/// SIXENSE_EXPORT int sixenseSetActiveBase( int i );
/// SIXENSE_EXPORT int sixenseIsBaseConnected( int i );
/// 
/// SIXENSE_EXPORT int sixenseGetMaxControllers( void );
/// SIXENSE_EXPORT int sixenseIsControllerEnabled( int which );
/// SIXENSE_EXPORT int sixenseGetNumActiveControllers();
/// 
/// SIXENSE_EXPORT int sixenseGetHistorySize();
/// 
/// SIXENSE_EXPORT int sixenseGetData( int which, int index_back, sixenseControllerData * );
/// SIXENSE_EXPORT int sixenseGetAllData( int index_back, sixenseAllControllerData * );
/// SIXENSE_EXPORT int sixenseGetNewestData( int which, sixenseControllerData * );
/// SIXENSE_EXPORT int sixenseGetAllNewestData( sixenseAllControllerData * );
/// 
/// SIXENSE_EXPORT int sixenseSetHemisphereTrackingMode( int which_controller, int state );
/// SIXENSE_EXPORT int sixenseGetHemisphereTrackingMode( int which_controller, int *state );
/// 
/// SIXENSE_EXPORT int sixenseAutoEnableHemisphereTracking( int which_controller );
/// 
/// SIXENSE_EXPORT int sixenseSetHighPriorityBindingEnabled( int on_or_off );
/// SIXENSE_EXPORT int sixenseGetHighPriorityBindingEnabled( int *on_or_off );
/// 
/// SIXENSE_EXPORT int sixenseTriggerVibration( int controller_id, int duration_100ms, int pattern_id );
/// 
/// SIXENSE_EXPORT int sixenseSetFilterEnabled( int on_or_off );
/// SIXENSE_EXPORT int sixenseGetFilterEnabled( int *on_or_off );
/// 
/// SIXENSE_EXPORT int sixenseSetFilterParams( float near_range, float near_val, float far_range, float far_val );
/// SIXENSE_EXPORT int sixenseGetFilterParams( float *near_range, float *near_val, float *far_range, float *far_val );
/// 
/// SIXENSE_EXPORT int sixenseSetBaseColor( unsigned char red, unsigned char green, unsigned char blue );
/// SIXENSE_EXPORT int sixenseGetBaseColor( unsigned char *red, unsigned char *green, unsigned char *blue );
/// 
/// #if defined(__LANGUAGE_C_PLUS_PLUS)||defined(__cplusplus)||defined(c_plusplus)
/// }
/// #endif
/// 
/// #endif
/// </code>
public partial class SixensePlugin
{
#if DEBUG
    public const string SIXENSE_DLL = "sixensed_x64";
#else
    public const string SIXENSE_DLL = "sixense_x64";
    //public const string SIXENSE_DLL = "sixense";

#endif

	[StructLayout( LayoutKind.Sequential )]
	public struct sixenseControllerData
	{
		[MarshalAs( UnmanagedType.ByValArray, SizeConst = 3 )]
		public float[] pos;
		[MarshalAs( UnmanagedType.ByValArray, SizeConst = 9 )]
		public float[] rot_mat;
		public float joystick_x;
		public float joystick_y;
		public float trigger;
		public uint buttons;
		public byte sequence_number;
		[MarshalAs( UnmanagedType.ByValArray, SizeConst = 4 )]
		public float[] rot_quat;
		public ushort firmware_revision;
		public ushort hardware_revision;
		public ushort packet_type;
		public ushort magnetic_frequency;
		public int enabled;
		public int controller_index;
		public byte is_docked;
		public byte which_hand;
		public byte hemi_tracking_enabled;
	}
	
	[StructLayout( LayoutKind.Sequential )]
	public struct sixenseAllControllerData
	{
		[MarshalAs( UnmanagedType.ByValArray, SizeConst = 4 )]
		public sixenseControllerData[] controllers;
	}

    [DllImport( SIXENSE_DLL, EntryPoint = "sixenseInit", CallingConvention = CallingConvention.Cdecl)]
	public static extern int sixenseInit();
	
	[DllImport( SIXENSE_DLL, EntryPoint = "sixenseExit", CallingConvention = CallingConvention.Cdecl)]
	public static extern int sixenseExit();
	
	[DllImport( SIXENSE_DLL, EntryPoint = "sixenseGetMaxBases", CallingConvention = CallingConvention.Cdecl)]
	public static extern int sixenseGetMaxBases();
	
	[DllImport( SIXENSE_DLL, EntryPoint = "sixenseSetActiveBase", CallingConvention = CallingConvention.Cdecl)]
	public static extern int sixenseSetActiveBase( int i );
	
	[DllImport( SIXENSE_DLL, EntryPoint = "sixenseIsBaseConnected", CallingConvention = CallingConvention.Cdecl)]
	public static extern int sixenseIsBaseConnected( int i );
	
	[DllImport( SIXENSE_DLL, EntryPoint = "sixenseGetMaxControllers", CallingConvention = CallingConvention.Cdecl)]
	public static extern int sixenseGetMaxControllers();
	
	[DllImport( SIXENSE_DLL, EntryPoint = "sixenseIsControllerEnabled", CallingConvention = CallingConvention.Cdecl)]
	public static extern int sixenseIsControllerEnabled( int which );
	
	[DllImport( SIXENSE_DLL, EntryPoint = "sixenseGetNumActiveControllers", CallingConvention = CallingConvention.Cdecl)]
	public static extern int sixenseGetNumActiveControllers();
	
	[DllImport( SIXENSE_DLL, EntryPoint = "sixenseGetHistorySize", CallingConvention = CallingConvention.Cdecl)]
	public static extern int sixenseGetHistorySize();
	
	[DllImport( SIXENSE_DLL, EntryPoint = "sixenseGetData", CallingConvention = CallingConvention.Cdecl)]
	public static extern int sixenseGetData( int which, int index_back, ref sixenseControllerData cd );
	
	[DllImport( SIXENSE_DLL, EntryPoint = "sixenseGetAllData", CallingConvention = CallingConvention.Cdecl)]
	public static extern int sixenseGetAllData( int index_back, ref sixenseAllControllerData acd );
	
	[DllImport( SIXENSE_DLL, EntryPoint = "sixenseGetNewestData", CallingConvention = CallingConvention.Cdecl)]
	public static extern int sixenseGetNewestData( int which, ref sixenseControllerData cd );
	
	[DllImport( SIXENSE_DLL, EntryPoint = "sixenseGetAllNewestData", CallingConvention = CallingConvention.Cdecl)]
	public static extern int sixenseGetAllNewestData( ref sixenseAllControllerData acd );
	
	[DllImport( SIXENSE_DLL, EntryPoint = "sixenseSetHemisphereTrackingMode", CallingConvention = CallingConvention.Cdecl)]
	public static extern int sixenseSetHemisphereTrackingMode( int which_controller, int state );
	
	[DllImport( SIXENSE_DLL, EntryPoint = "sixenseGetHemisphereTrackingMode", CallingConvention = CallingConvention.Cdecl)]
	public static extern int sixenseGetHemisphereTrackingMode( int which_controller, ref int state );
	
	[DllImport( SIXENSE_DLL, EntryPoint = "sixenseAutoEnableHemisphereTracking", CallingConvention = CallingConvention.Cdecl)]
	public static extern int sixenseAutoEnableHemisphereTracking( int which_controller );
	
	[DllImport( SIXENSE_DLL, EntryPoint = "sixenseSetHighPriorityBindingEnabled", CallingConvention = CallingConvention.Cdecl)]
	public static extern int sixenseSetHighPriorityBindingEnabled( int on_or_off );
	
	[DllImport( SIXENSE_DLL, EntryPoint = "sixenseGetHighPriorityBindingEnabled", CallingConvention = CallingConvention.Cdecl)]
	public static extern int sixenseGetHighPriorityBindingEnabled( ref int on_or_off );
	
	[DllImport( SIXENSE_DLL, EntryPoint = "sixenseTriggerVibration", CallingConvention = CallingConvention.Cdecl)]
	public static extern int sixenseTriggerVibration( int controller_id, int duration_100ms, int pattern_id );
	
	[DllImport( SIXENSE_DLL, EntryPoint = "sixenseSetFilterEnabled", CallingConvention = CallingConvention.Cdecl)]
	public static extern int sixenseSetFilterEnabled( int on_or_off );
	
	[DllImport( SIXENSE_DLL, EntryPoint = "sixenseGetFilterEnabled", CallingConvention = CallingConvention.Cdecl)]
	public static extern int sixenseGetFilterEnabled( ref int on_or_off );
	
	[DllImport( SIXENSE_DLL, EntryPoint = "sixenseSetFilterParams", CallingConvention = CallingConvention.Cdecl)]
	public static extern int sixenseSetFilterParams( float near_range, float near_val, float far_range, float far_val );
	
	[DllImport( SIXENSE_DLL, EntryPoint = "sixenseGetFilterParams", CallingConvention = CallingConvention.Cdecl)]
	public static extern int sixenseGetFilterParams( ref float near_range, ref float near_val, ref float far_range, ref float far_val );
	
	[DllImport( SIXENSE_DLL, EntryPoint = "sixenseSetBaseColor", CallingConvention = CallingConvention.Cdecl)]
	public static extern int sixenseSetBaseColor( byte red, byte green, byte blue );
	
	[DllImport( SIXENSE_DLL, EntryPoint = "sixenseGetBaseColor", CallingConvention = CallingConvention.Cdecl)]
	public static extern int sixenseGetBaseColor( ref byte red, ref byte green, ref byte blue );
}
