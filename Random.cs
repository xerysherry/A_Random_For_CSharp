/* Copyright 2017 xerysherry
 * 
 * Permission is hereby granted, free of charge, to any person 
 * obtaining a copy of this software and associated documentation 
 * files (the "Software"), to deal in the Software without restriction, 
 * including without limitation the rights to use, copy, modify, 
 * merge, publish, distribute, sublicense, and/or sell copies of 
 * the Software, and to permit persons to whom the Software is 
 * furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall 
 * be included in all copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, 
 * EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES 
 * OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. 
 * IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE 
 * FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION 
 * OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR 
 * IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS 
 * IN THE SOFTWARE.
 */

public class Random
{
    public Random()
    {
        var tick = System.DateTime.Now.Ticks;
        var h = (uint)((tick >> 32) & 0xFFFFFFFF);
        var l = (uint)(tick & 0xFFFFFFFF);
        z = (l & 0xFFFF0000) + ((h >> 16) & 0xFFFF);
        w = ((h & 0xFFFF) << 16) + (l & 0xFFFF);
        i = 0;
    }
    public Random(uint seed)
    {
        z = (seed >> 16);
        z *= z;
        w = (seed & 0xFFFF);
        w ^= 0xFFFF;
        w *= w;
        i = 0;
    }

    public int Next()
    {
        return (int)(GetUint() & 0x7fffffff);
    }
    public int Next(int max)
    {
        return (int)(GetUint() % max);
    }
    public int Next(int min, int max)
    {
        return min + Next(max - min);
    }
    public void NextBytes(byte[] buffer)
    {
        if (buffer == null)
            return;
        var l = buffer.Length;
        for (int i = 0; i < l; ++i)
        {
            buffer[i] = (byte)Next(256);
        }
    }
    public double NextDouble()
    {
        // 0 <= u < 2^32
        uint u = GetUint();
        // The magic number below is 1/(2^32 + 2).
        // The result is strictly between 0 and 1.
        return (u + 1.0) * 2.328306435454494e-10;
    }

    private uint GetUint()
    {
        i += 1;
        z = 36969 * (z & 65535) + (z >> 16);
        w = 18000 * (w & 65535) + (w >> 16);
        return (z << 16) + w;
    }
    uint z = 0;
    uint w = 0;
    uint i = 0;
}
