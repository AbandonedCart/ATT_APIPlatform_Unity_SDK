using UnityEngine;
using System.Collections;
using System.IO;

public class WaveGen
{
	public WaveFormatChunk MakeFormat (AudioClip clip)
	{
		WaveFormatChunk format = new WaveFormatChunk (8000, 1);
		return format;
	}

	public void Write (float[] clipData, WaveFormatChunk format, FileStream stream)
	{
		WaveHeader header = new WaveHeader ();
		WaveDataChunk data = new WaveDataChunk ();
		
		data.shortArray = new short[clipData.Length];
		for (int i = 0; i < clipData.Length; i++)
			data.shortArray [i] = (short)(clipData [i] * 32767);
		
		data.dwChunkSize = (uint)(data.shortArray.Length * (format.wBitsPerSample / 8));
		
		BinaryWriter writer = new BinaryWriter (stream);
		writer.Write (header.sGroupID.ToCharArray ());
		writer.Write (header.dwFileLength);
		writer.Write (header.sRiffType.ToCharArray ());
		writer.Write (format.sChunkID.ToCharArray ());
		writer.Write (format.dwChunkSize);
		writer.Write (format.wFormatTag);
		writer.Write (format.wChannels);
		writer.Write (format.dwSamplesPerSec);
		writer.Write (format.dwAvgBytesPerSec);
		writer.Write (format.wBlockAlign);
		writer.Write (format.wBitsPerSample);
		writer.Write (data.sChunkID.ToCharArray ());
		writer.Write (data.dwChunkSize);
		foreach (short dataPoint in data.shortArray) {
			writer.Write (dataPoint);
		}
		writer.Seek (4, SeekOrigin.Begin);
		uint filesize = (uint)writer.BaseStream.Length;
		writer.Write (filesize - 8);
		writer.Close ();
	}
	
	public class WaveDataChunk
	{
		public string sChunkID;     // "data"
		public uint dwChunkSize;    // Length of header in bytes
		public short[] shortArray;  // 8-bit audio
 
		public WaveDataChunk ()
		{
			shortArray = new short[0];
			dwChunkSize = 0;
			sChunkID = "data";
		}   
	}
	
	public class WaveFormatChunk
	{
		public string sChunkID;
		public uint dwChunkSize;
		public ushort wFormatTag;
		public ushort wChannels;
		public uint dwSamplesPerSec;
		public uint dwAvgBytesPerSec;
		public ushort wBlockAlign;
		public ushort wBitsPerSample;
 
		public WaveFormatChunk (float sps, int ch)
		{
			sChunkID = "fmt ";
			dwChunkSize = 16;
			wFormatTag = 1;
			wChannels = (ushort)ch;
			dwSamplesPerSec = (uint)sps;
			wBitsPerSample = 16;
			wBlockAlign = (ushort)(wChannels * (wBitsPerSample / 8));
			dwAvgBytesPerSec = dwSamplesPerSec * wBlockAlign;            
		}
	}

	public class WaveHeader
	{
		public string sGroupID;
		public uint dwFileLength;
		public string sRiffType;
 
		public WaveHeader ()
		{
			dwFileLength = 0;
			sGroupID = "RIFF";
			sRiffType = "WAVE";
		}
	}
}
