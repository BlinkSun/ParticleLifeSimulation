/* This class has been written by
 * Corinna John (Hannover, Germany)
 * cj@binary-universe.net
 * 
 * You may do with this code whatever you like,
 * except selling it or claiming any rights/ownership.
 * 
 * Please send me a little feedback about what you're
 * using this code for and what changes you'd like to
 * see in later versions. (And please excuse my bad english.)
 * 
 * WARNING: This is experimental code.
 * Please do not expect "Release Quality".
 * */

#region Using directives

using System.Runtime.InteropServices;

#endregion

namespace ParticleLifeSimulation.AviFile
{
    public class EditableVideoStream : VideoStream
    {
        private IntPtr editableStream = IntPtr.Zero;

        /// <summary>Pointer to the unmanaged AVI stream</summary>
        internal override IntPtr StreamPointer
        {
            get { return editableStream; }
        }

        /// <summary>Create an editable stream from an uneditable stream</summary>
        /// <param name="stream">uneditable stream</param>
        public EditableVideoStream(VideoStream stream) : base(stream.FrameSize, stream.FrameRate, stream.Width, stream.Height, stream.CountBitsPerPixel, stream.CountFrames, stream.CompressOptions, stream.WriteCompressed)
        {
            Avi.AVIFileInit();
            Avi.CreateEditableStream(ref editableStream, stream.StreamPointer);
            SetInfo(stream.StreamInfo);
        }

        /// <summary>Close the stream</summary>
        public override void Close()
        {
            base.Close();
            Avi.AVIFileExit();
        }

        /// <summary>Copy a number of frames into a temporary stream</summary>
        /// <param name="start">First frame to copy</param>
        /// <param name="length">Count of frames to copy</param>
        /// <returns>Pointer to the unmanaged temporary stream</returns>
        public IntPtr Copy(int start, int length)
        {
            IntPtr result = IntPtr.Zero;
            Avi.EditStreamCopy(editableStream, ref start, ref length, ref result);
            return result;
        }

        /// <summary>Move a number of frames into a temporary stream</summary>
        /// <param name="start">First frame to cut</param>
        /// <param name="length">Count of frames to cut</param>
        /// <returns>Pointer to the unmanaged temporary stream</returns>
        public IntPtr Cut(int start, int length)
        {
            IntPtr result = IntPtr.Zero;
            Avi.EditStreamCut(editableStream, ref start, ref length, ref result);
            countFrames -= length;
            return result;
        }

        /// <summary>Paste a number of frames from another video stream into this stream</summary>
        /// <param name="sourceStream">Stream to copy from</param>
        /// <param name="copyPosition">Index of the first frame to copy</param>
        /// <param name="pastePosition">Where to paste the copied frames</param>
        /// <param name="length">Count of frames to paste</param>
        public void Paste(VideoStream sourceStream, int copyPosition, int pastePosition, int length)
        {
            Paste(sourceStream.StreamPointer, copyPosition, pastePosition, length);
        }

        /// <summary>Paste a number of frames from another video stream into this stream</summary>
        /// <param name="sourceStream">Pointer to the unmanaged stream to copy from</param>
        /// <param name="copyPosition">Index of the first frame to copy</param>
        /// <param name="pastePosition">Where to paste the copied frames</param>
        /// <param name="length">Count of frames to paste</param>
        public void Paste(IntPtr sourceStream, int copyPosition, int pastePosition, int length)
        {
            int pastedLength = 0;
            int hResult = Avi.EditStreamPaste(editableStream, ref pastePosition, ref pastedLength, sourceStream, copyPosition, length);
            countFrames += pastedLength;
        }

        /// <summary>Change the AviStreamInfo values and update the frame rate</summary>
        /// <param name="info"></param>
        public void SetInfo(Avi.AVISTREAMINFO info)
        {
            Avi.EditStreamSetInfo(editableStream, ref info, Marshal.SizeOf(info));
            frameRate = info.dwRate / info.dwScale;
        }
    }
}
