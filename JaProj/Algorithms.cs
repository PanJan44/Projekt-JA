using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Numerics;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

namespace JaProj
{
    public class Algorithms
    {
        public Action<byte[], byte[], Vector4> FuncToCall { get; set; }

        public Algorithms(int choice)
        {
            if (choice == 1)
                FuncToCall = BlurFragmentCSharp;
            else FuncToCall = BlurAsm;
        }

        public byte[] BlurImage(byte[] sourceData, int width, int height, int r, int threadCount)
        {
            var tasks = new List<Task>();
            byte[] outData = new byte[sourceData.Length * 3];

            var calcBar = height / threadCount;
            var remainder = height % threadCount;

            var calcBarPerTask = Enumerable.Repeat(calcBar, threadCount).ToList();

            for (int i = remainder; i > 0; i--)
                calcBarPerTask[i]++;

            for (int i = 0; i < threadCount; i++)
            {
                var tidx = i;
                var startHeight = calcBarPerTask.Take(tidx).Sum();
                var endHeight = startHeight + calcBarPerTask[tidx];

                if (startHeight + r > endHeight)
                    throw new InvalidOperationException("Nieporawnie ustawione parametry!");

                tasks.Add(Task.Run(() => FuncToCall(sourceData, outData, new Vector4(width,
                    tidx == 0 ? startHeight + r : startHeight,
                    tidx == threadCount - 1 ? endHeight - r : endHeight, r))));
            }

            Task.WaitAll(tasks.ToArray());
            return outData;
        }

        private static void BlurFragmentCSharp(byte[] sourceData, byte[] outData, Vector4 vector4)
        {
            float sumR;
            float sumG;
            float sumB;
            int startHeight = (int)vector4.Y;
            int endHeight = (int)vector4.Z;
            int width = (int)vector4.X;
            int r = (int)vector4.W;
            int n = 2 * r + 1;
            float n1 = n * n;

            for (int i = startHeight; i < endHeight; i++)
            {
                for (int j = 3 * r; j < width - 3 * r; j += 3)
                {
                    sumR = 0;
                    sumG = 0;
                    sumB = 0;

                    for (int k = -r; k <= r; k++)
                    {
                        for (int l = -3 * r; l <= 3 * r; l += 3)
                        {
                            sumR += sourceData[(i + k) * width + j + l + 2];
                            sumG += sourceData[(i + k) * width + j + l + 1];
                            sumB += sourceData[(i + k) * width + j + l];
                        }
                    }
                    outData[i * width + j + 2] = (byte)(sumR / n1);
                    outData[i * width + j + 1] = (byte)(sumG / n1);
                    outData[i * width + j] = (byte)(sumB / n1);
                }
            }
        }

        [DllImport("JAAsm.dll")]
        private static extern void BlurAsm(byte[] sourceData, byte[] outData, Vector4 vector4);

    }

}

