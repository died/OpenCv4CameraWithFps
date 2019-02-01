using System.Diagnostics;
using OpenCvSharp;

namespace OpenCv4CameraTest
{
    class Program
    {
        static void Main()
        {
            var cap = new VideoCapture(CaptureDevice.Any);
            //double fps = 60;
            //cap.Set(CaptureProperty.Fps, fps);    //I think fps set didn't work on my webcam
            int sleepTime = 1;  //(int)Math.Round(1000 / fps);
            var sw = Stopwatch.StartNew();
            var counter = 0;
            using (Window window = new Window("capture"))
            using (Mat image = new Mat())
            {
                while (true)
                {
                    cap.Read(image);
                    if (image.Empty())
                        break;
                    counter++;
                    double frame = (double) counter / sw.ElapsedMilliseconds * 1000;
                    Cv2.PutText(image, $"FPS:{frame:F}", new Point(10,40), HersheyFonts.HersheySimplex, 1, Scalar.Red);
                    window.ShowImage(image);
                    Cv2.WaitKey(sleepTime);

                    if (counter % 1299721 == 0) //random pick 100001st prime
                    {
                        counter = 0;
                        sw.Reset();
                    }
                }
            }
            cap.Release();
        }
    }
}
