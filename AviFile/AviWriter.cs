using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace ParticleLifeSimulation.AviFile
{
    // x64
    public class AviWriter
    {
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        private struct AVISTREAMINFOW
        {
            public UInt32 fccType, fccHandler, dwFlags, dwCaps;

            public UInt16 wPriority, wLanguage;

            public UInt32 dwScale, dwRate,
                             dwStart, dwLength, dwInitialFrames, dwSuggestedBufferSize,
                             dwQuality, dwSampleSize, rect_left, rect_top,
                             rect_right, rect_bottom, dwEditCount, dwFormatChangeCount;

            public UInt16 szName0, szName1, szName2, szName3, szName4, szName5,
                             szName6, szName7, szName8, szName9, szName10, szName11,
                             szName12, szName13, szName14, szName15, szName16, szName17,
                             szName18, szName19, szName20, szName21, szName22, szName23,
                             szName24, szName25, szName26, szName27, szName28, szName29,
                             szName30, szName31, szName32, szName33, szName34, szName35,
                             szName36, szName37, szName38, szName39, szName40, szName41,
                             szName42, szName43, szName44, szName45, szName46, szName47,
                             szName48, szName49, szName50, szName51, szName52, szName53,
                             szName54, szName55, szName56, szName57, szName58, szName59,
                             szName60, szName61, szName62, szName63;
        }
        // vfw.h
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        private struct AVICOMPRESSOPTIONS
        {
            public UInt32 fccType;
            public UInt32 fccHandler;
            public UInt32 dwKeyFrameEvery;  // only used with AVICOMRPESSF_KEYFRAMES
            public UInt32 dwQuality;
            public UInt32 dwBytesPerSecond; // only used with AVICOMPRESSF_DATARATE
            public UInt32 dwFlags;
            public IntPtr lpFormat;
            public UInt32 cbFormat;
            public IntPtr lpParms;
            public UInt32 cbParms;
            public UInt32 dwInterleaveEvery;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct BITMAPINFOHEADER
        {
            public UInt32 biSize;
            public Int32 biWidth;
            public Int32 biHeight;
            public Int16 biPlanes;
            public Int16 biBitCount;
            public UInt32 biCompression;
            public UInt32 biSizeImage;
            public Int32 biXPelsPerMeter;
            public Int32 biYPelsPerMeter;
            public UInt32 biClrUsed;
            public UInt32 biClrImportant;
        }

        public class AviException : ApplicationException
        {
            public AviException(string s) : base(s) { }
            public AviException(string s, Int32 hr) : base(s)
            {

                if (hr == AVIERR_BADPARAM)
                {
                    err_msg = "AVIERR_BADPARAM";
                }
                else
                {
                    err_msg = "unknown";
                }
            }

            public string ErrMsg()
            {
                return err_msg ?? String.Empty;
            }
            private const Int32 AVIERR_BADPARAM = -2147205018;
            private readonly string? err_msg;
        }

        public Bitmap Open(string fileName, UInt32 frameRate, int width, int height)
        {
            frameRate_ = frameRate;
            width_ = (UInt32)width;
            height_ = (UInt32)height;
            bmp_ = new Bitmap(width, height, PixelFormat.Format24bppRgb);
            BitmapData bmpDat = bmp_.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);
            stride_ = (UInt32)bmpDat.Stride;
            bmp_.UnlockBits(bmpDat);
            AVIFileInit();
            int hr = AVIFileOpenW(ref pfile_, fileName, 4097 /* OF_WRITE | OF_CREATE (winbase.h) */, 0);
            if (hr != 0)
            {
                throw new AviException("error for AVIFileOpenW");
            }

            CreateStream();
            SetOptions();

            return bmp_;
        }

        public void AddFrame()
        {

            BitmapData bmpDat = bmp_.LockBits(
              new Rectangle(0, 0, (int)width_, (int)height_), ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);

            int hr = AVIStreamWrite(psCompressed_, count_, 1,
               bmpDat.Scan0, // pointer to data
                             //(Int32)(stride_ * height_),
               (Int32)(bmpDat.Stride * height_),
               0, // 16 = AVIIF_KEYFRAMe
               0,
               0);

            if (hr != 0)
            {
                throw new AviException("AVIStreamWrite");
            }

            bmp_.UnlockBits(bmpDat);

            count_++;
        }

        public void Close()
        {
            _ = AVIStreamRelease(ps_);
            _ = AVIStreamRelease(psCompressed_);

            _ = AVIFileRelease(pfile_);
            AVIFileExit();
        }

        private void CreateStream()
        {
            AVISTREAMINFOW strhdr = new()
            {
                fccType = fccType_,
                fccHandler = fccHandler_,
                dwFlags = 0,
                dwCaps = 0,
                wPriority = 0,
                wLanguage = 0,
                dwScale = 1,
                dwRate = frameRate_, // Frames per Second
                dwStart = 0,
                dwLength = 0,
                dwInitialFrames = 0,
                dwSuggestedBufferSize = height_ * stride_,
                dwQuality = 0xffffffff, //-1;         // Use default
                dwSampleSize = 0,
                rect_top = 0,
                rect_left = 0,
                rect_bottom = height_,
                rect_right = width_,
                dwEditCount = 0,
                dwFormatChangeCount = 0,
                szName0 = 0,
                szName1 = 0
            };

            int hr = AVIFileCreateStream(pfile_, out ps_, ref strhdr);

            if (hr != 0)
            {
                throw new AviException("AVIFileCreateStream");
            }
        }

        private unsafe void SetOptions()
        {
            AVICOMPRESSOPTIONS opts = new()
            {
                fccType = 0, //fccType_;
                fccHandler = 0,//fccHandler_;
                dwKeyFrameEvery = 0,
                dwQuality = 0,  // 0 .. 10000
                dwFlags = 0,  // AVICOMRPESSF_KEYFRAMES = 4
                dwBytesPerSecond = 0,
                lpFormat = IntPtr.Zero,
                cbFormat = 0,
                lpParms = IntPtr.Zero,
                cbParms = 0,
                dwInterleaveEvery = 0
            };

            AVICOMPRESSOPTIONS* p = &opts;
            AVICOMPRESSOPTIONS** pp = &p;

            IntPtr x = ps_;
            IntPtr* ptr_ps = &x;

            _ = AVISaveOptions(0, 0, 1, ptr_ps, pp);

            // TODO: AVISaveOptionsFree(...)

            int hr = AVIMakeCompressedStream(out psCompressed_, ps_, ref opts, 0);
            if (hr != 0)
            {
                throw new AviException("AVIMakeCompressedStream");
            }

            BITMAPINFOHEADER bi = new()
            {
                biSize = 40,
                biWidth = (Int32)width_,
                biHeight = (Int32)height_,
                biPlanes = 1,
                biBitCount = 24,
                biCompression = 0,  // 0 = BI_RGB
                biSizeImage = stride_ * height_,
                biXPelsPerMeter = 0,
                biYPelsPerMeter = 0,
                biClrUsed = 0,
                biClrImportant = 0
            };

            hr = AVIStreamSetFormat(psCompressed_, 0, ref bi, 40);
            if (hr != 0)
            {
                throw new AviException("AVIStreamSetFormat", hr);
            }
        }

        [DllImport("avifil32.dll")]
        private static extern void AVIFileInit();

        [DllImport("avifil32.dll")]
        private static extern int AVIFileOpenW(ref IntPtr ptr_pfile, [MarshalAs(UnmanagedType.LPWStr)] string fileName, int flags, int dummy);

        [DllImport("avifil32.dll")]
        private static extern int AVIFileCreateStream(IntPtr ptr_pfile, out IntPtr ptr_ptr_avi, ref AVISTREAMINFOW ptr_streaminfo);

        [DllImport("avifil32.dll")]
        private static extern int AVIMakeCompressedStream(out IntPtr ppsCompressed, IntPtr aviStream, ref AVICOMPRESSOPTIONS ao, int dummy);

        [DllImport("avifil32.dll")]
        private static extern int AVIStreamSetFormat(IntPtr aviStream, Int32 lPos, ref BITMAPINFOHEADER lpFormat, Int32 cbFormat);

        [DllImport("avifil32.dll")]
        private static extern unsafe int AVISaveOptions(int hwnd, UInt32 flags, int nStreams, IntPtr* ptr_ptr_avi, AVICOMPRESSOPTIONS** ao);

        [DllImport("avifil32.dll")]
        private static extern int AVIStreamWrite(IntPtr aviStream, Int32 lStart, Int32 lSamples, IntPtr lpBuffer, Int32 cbBuffer, Int32 dwFlags, Int32 dummy1, Int32 dummy2);

        [DllImport("avifil32.dll")]
        private static extern int AVIStreamRelease(IntPtr aviStream);

        [DllImport("avifil32.dll")]
        private static extern int AVIFileRelease(IntPtr pfile);

        [DllImport("avifil32.dll")]
        private static extern void AVIFileExit();

        //private int pfile_ = 0;
        private IntPtr pfile_ = IntPtr.Zero;
        private IntPtr ps_ = IntPtr.Zero;
        private IntPtr psCompressed_ = IntPtr.Zero;
        private UInt32 frameRate_ = 0;
        private int count_ = 0;
        private UInt32 width_ = 0;
        private UInt32 stride_ = 0;
        private UInt32 height_ = 0;
        private readonly UInt32 fccType_ = 1935960438;  // vids
        private readonly UInt32 fccHandler_ = 808810089;// IV50
                                                        //1145656899;  // CVID
        private Bitmap? bmp_;
    };
}
