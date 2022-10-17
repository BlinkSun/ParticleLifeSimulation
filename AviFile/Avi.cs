using System.Runtime.InteropServices;

namespace ParticleLifeSimulation.AviFile
{
    public class Avi
    {

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct RGBQUAD
        {
            public byte rgbBlue;
            public byte rgbGreen;
            public byte rgbRed;
            public byte rgbReserved;
        }

        public static int RGBQUAD_SIZE = 4;
        public static int PALETTE_SIZE = 4 * 256; //RGBQUAD * 256 colours

        public static readonly int streamtypeVIDEO = mmioFOURCC('v', 'i', 'd', 's');
        public static readonly int streamtypeAUDIO = mmioFOURCC('a', 'u', 'd', 's');
        public static readonly int streamtypeMIDI = mmioFOURCC('m', 'i', 'd', 's');
        public static readonly int streamtypeTEXT = mmioFOURCC('t', 'x', 't', 's');

        public const int OF_SHARE_DENY_WRITE = 32;
        public const int OF_WRITE = 1;
        public const int OF_READWRITE = 2;
        public const int OF_CREATE = 4096;

        public const int BMP_MAGIC_COOKIE = 19778; //ascii string "BM"

        public const int AVICOMPRESSF_INTERLEAVE = 0x00000001;    // interleave
        public const int AVICOMPRESSF_DATARATE = 0x00000002;    // use a data rate
        public const int AVICOMPRESSF_KEYFRAMES = 0x00000004;    // use keyframes
        public const int AVICOMPRESSF_VALID = 0x00000008;    // has valid data
        public const int AVIIF_KEYFRAME = 0x00000010;

        public const uint ICMF_CHOOSE_KEYFRAME = 0x0001;  // show KeyFrame Every box
        public const uint ICMF_CHOOSE_DATARATE = 0x0002;  // show DataRate box
        public const uint ICMF_CHOOSE_PREVIEW = 0x0004;	// allow expanded preview dialog

        //macro mmioFOURCC
        public static int mmioFOURCC(char ch0, char ch1, char ch2, char ch3)
        {
            return ((int)(byte)(ch0) | ((byte)(ch1) << 8) |
                ((byte)(ch2) << 16) | ((byte)(ch3) << 24));
        }

        #region structure declarations

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct RECT
        {
            public uint left;
            public uint top;
            public uint right;
            public uint bottom;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct BITMAPINFOHEADER
        {
            public int biSize;
            public int biWidth;
            public int biHeight;
            public short biPlanes;
            public short biBitCount;
            public int biCompression;
            public int biSizeImage;
            public int biXPelsPerMeter;
            public int biYPelsPerMeter;
            public int biClrUsed;
            public int biClrImportant;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct BITMAPINFO
        {
            public BITMAPINFOHEADER bmiHeader;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 256)]
            public RGBQUAD[] bmiColors;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct PCMWAVEFORMAT
        {
            public short wFormatTag;
            public short nChannels;
            public int nSamplesPerSec;
            public int nAvgBytesPerSec;
            public short nBlockAlign;
            public short wBitsPerSample;
            public short cbSize;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct AVISTREAMINFO
        {
            public int fccType;
            public int fccHandler;
            public int dwFlags;
            public int dwCaps;
            public short wPriority;
            public short wLanguage;
            public int dwScale;
            public int dwRate;
            public int dwStart;
            public int dwLength;
            public int dwInitialFrames;
            public int dwSuggestedBufferSize;
            public int dwQuality;
            public int dwSampleSize;
            public RECT rcFrame;
            public int dwEditCount;
            public int dwFormatChangeCount;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 64)]
            public ushort[] szName;
        }
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct BITMAPFILEHEADER
        {
            public short bfType; //"magic cookie" - must be "BM"
            public int bfSize;
            public short bfReserved1;
            public short bfReserved2;
            public int bfOffBits;
        }


        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct AVIFILEINFO
        {
            public int dwMaxBytesPerSecond;
            public int dwFlags;
            public int dwCaps;
            public int dwStreams;
            public int dwSuggestedBufferSize;
            public int dwWidth;
            public int dwHeight;
            public int dwScale;
            public int dwRate;
            public int dwLength;
            public int dwEditCount;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 64)]
            public char[] szFileType;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct AVICOMPRESSOPTIONS
        {
            public uint fccType;
            public uint fccHandler;
            public uint dwKeyFrameEvery;  // only used with AVICOMRPESSF_KEYFRAMES
            public uint dwQuality;
            public uint dwBytesPerSecond; // only used with AVICOMPRESSF_DATARATE
            public uint dwFlags;
            public IntPtr lpFormat;
            public uint cbFormat;
            public IntPtr lpParms;
            public uint cbParms;
            public uint dwInterleaveEvery;
        }

        /// <summary>AviSaveV needs a pointer to a pointer to an AVICOMPRESSOPTIONS structure</summary>
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public class AVICOMPRESSOPTIONS_CLASS
        {
            public uint fccType;
            public uint fccHandler;
            public uint dwKeyFrameEvery;  // only used with AVICOMRPESSF_KEYFRAMES
            public uint dwQuality;
            public uint dwBytesPerSecond; // only used with AVICOMPRESSF_DATARATE
            public uint dwFlags;
            public IntPtr lpFormat;
            public uint cbFormat;
            public IntPtr lpParms;
            public uint cbParms;
            public uint dwInterleaveEvery;

            public AVICOMPRESSOPTIONS ToStruct()
            {
                AVICOMPRESSOPTIONS returnVar = new()
                {
                    fccType = this.fccType,
                    fccHandler = this.fccHandler,
                    dwKeyFrameEvery = this.dwKeyFrameEvery,
                    dwQuality = this.dwQuality,
                    dwBytesPerSecond = this.dwBytesPerSecond,
                    dwFlags = this.dwFlags,
                    lpFormat = this.lpFormat,
                    cbFormat = this.cbFormat,
                    lpParms = this.lpParms,
                    cbParms = this.cbParms,
                    dwInterleaveEvery = this.dwInterleaveEvery
                };
                return returnVar;
            }
        }
        #endregion structure declarations

        #region method declarations

        //Initialize the AVI library
        [DllImport("avifil32.dll")]
        public static extern void AVIFileInit();

        //Open an AVI file
        [DllImport("avifil32.dll", PreserveSig = true)]
        public static extern int AVIFileOpen(
            ref int ppfile,
            string szFile,
            int uMode,
            int pclsidHandler);

        //Get a stream from an open AVI file
        [DllImport("avifil32.dll")]
        public static extern int AVIFileGetStream(
            int pfile,
            out IntPtr ppavi,
            int fccType,
            int lParam);

        //Get the start position of a stream
        [DllImport("avifil32.dll", PreserveSig = true)]
        public static extern int AVIStreamStart(int pavi);

        //Get the length of a stream in frames
        [DllImport("avifil32.dll", PreserveSig = true)]
        public static extern int AVIStreamLength(int pavi);

        //Get information about an open stream
        [DllImport("avifil32.dll")]
        public static extern int AVIStreamInfo(
            IntPtr pAVIStream,
            ref AVISTREAMINFO psi,
            int lSize);

        //Get a pointer to a GETFRAME object (returns 0 on error)
        [DllImport("avifil32.dll")]
        public static extern int AVIStreamGetFrameOpen(
            IntPtr pAVIStream,
            ref BITMAPINFOHEADER bih);

        //Get a pointer to a packed DIB (returns 0 on error)
        [DllImport("avifil32.dll")]
        public static extern int AVIStreamGetFrame(
            int pGetFrameObj,
            int lPos);

        //Create a new stream in an open AVI file
        [DllImport("avifil32.dll")]
        public static extern int AVIFileCreateStream(
            int pfile,
            out IntPtr ppavi,
            ref AVISTREAMINFO ptr_streaminfo);
        //static extern int AVIFileCreateStream(IntPtr pfile, out IntPtr ppavi, ref AVISTREAMINFO psi);
        //Create an editable stream
        [DllImport("avifil32.dll")]
        public static extern int CreateEditableStream(
            ref IntPtr ppsEditable,
            IntPtr psSource
        );

        //Cut samples from an editable stream
        [DllImport("avifil32.dll")]
        public static extern int EditStreamCut(
            IntPtr pStream,
            ref int plStart,
            ref int plLength,
            ref IntPtr ppResult
        );

        //Copy a part of an editable stream
        [DllImport("avifil32.dll")]
        public static extern int EditStreamCopy(
            IntPtr pStream,
            ref int plStart,
            ref int plLength,
            ref IntPtr ppResult
        );

        //Paste an editable stream into another editable stream
        [DllImport("avifil32.dll")]
        public static extern int EditStreamPaste(
            IntPtr pStream,
            ref int plPos,
            ref int plLength,
            IntPtr pstream,
            int lStart,
            int lLength
        );

        //Change a stream's header values
        [DllImport("avifil32.dll")]
        public static extern int EditStreamSetInfo(
            IntPtr pStream,
            ref AVISTREAMINFO lpInfo,
            int cbInfo
        );

        [DllImport("avifil32.dll")]
        public static extern int AVIMakeFileFromStreams(
            ref IntPtr ppfile,
            int nStreams,
            ref IntPtr papStreams
        );

        //Set the format for a new stream
        [DllImport("avifil32.dll")]
        public static extern int AVIStreamSetFormat(
            IntPtr aviStream, int lPos,
            //ref BITMAPINFOHEADER lpFormat,
            ref BITMAPINFO lpFormat,
            int cbFormat);

        //Set the format for a new stream
        [DllImport("avifil32.dll")]
        public static extern int AVIStreamSetFormat(
            IntPtr aviStream, int lPos,
            ref PCMWAVEFORMAT lpFormat, int cbFormat);

        //Read the format for a stream
        [DllImport("avifil32.dll")]
        public static extern int AVIStreamReadFormat(
            IntPtr aviStream, int lPos,
            ref BITMAPINFO lpFormat, ref int cbFormat
            );

        //Read the size of the format for a stream
        [DllImport("avifil32.dll")]
        public static extern int AVIStreamReadFormat(
            IntPtr aviStream, int lPos,
            int empty, ref int cbFormat
            );

        //Read the format for a stream
        [DllImport("avifil32.dll")]
        public static extern int AVIStreamReadFormat(
            IntPtr aviStream, int lPos,
            ref PCMWAVEFORMAT lpFormat, ref int cbFormat
            );

        //Write a sample to a stream
        [DllImport("avifil32.dll")]
        public static extern int AVIStreamWrite(
            IntPtr aviStream, int lStart, int lSamples,
            IntPtr lpBuffer, int cbBuffer, int dwFlags,
            int dummy1, int dummy2);

        //Release the GETFRAME object
        [DllImport("avifil32.dll")]
        public static extern int AVIStreamGetFrameClose(
            int pGetFrameObj);

        //Release an open AVI stream
        [DllImport("avifil32.dll")]
        public static extern int AVIStreamRelease(IntPtr aviStream);

        //Release an open AVI file
        [DllImport("avifil32.dll")]
        public static extern int AVIFileRelease(int pfile);

        //Close the AVI library
        [DllImport("avifil32.dll")]
        public static extern void AVIFileExit();

        [DllImport("avifil32.dll")]
        public static extern int AVIMakeCompressedStream(
            out IntPtr ppsCompressed, IntPtr aviStream,
            ref AVICOMPRESSOPTIONS ao, int dummy);

        [DllImport("avifil32.dll")]
        public static extern bool AVISaveOptions(
            IntPtr hwnd,
            uint uiFlags,
            int nStreams,
            ref IntPtr ppavi,
            ref AVICOMPRESSOPTIONS_CLASS plpOptions
            );

        [DllImport("avifil32.dll")]
        public static extern long AVISaveOptionsFree(
            int nStreams,
            ref AVICOMPRESSOPTIONS_CLASS plpOptions
            );

        [DllImport("avifil32.dll")]
        public static extern int AVIFileInfo(
            int pfile,
            ref AVIFILEINFO pfi,
            int lSize);

        [DllImport("winmm.dll", EntryPoint = "mmioStringToFOURCCA")]
        public static extern int mmioStringToFOURCC(string sz, int uFlags);

        [DllImport("avifil32.dll")]
        public static extern int AVIStreamRead(
            IntPtr pavi,
            int lStart,
            int lSamples,
            IntPtr lpBuffer,
            int cbBuffer,
            int plBytes,
            int plSamples
            );

        [DllImport("avifil32.dll")]
        public static extern int AVISaveV(
            string szFile,
            short empty,
            short lpfnCallback,
            short nStreams,
            ref IntPtr ppavi,
            ref AVICOMPRESSOPTIONS_CLASS plpOptions
            );
        #endregion method declarations

    }
}
