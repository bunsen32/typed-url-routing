// -----------------------------------------------------------------------
// Adapted from (Public Domain?) code at: http://stackoverflow.com/a/769093/214560
// -----------------------------------------------------------------------
namespace Dysphoria.Net.UrlRouting.Test
{
	using System.Collections.Generic;
	using System.IO;
	using System.Text;

	// Implements multipart/form-data POST in C# http://stackoverflow.com/a/769093/214560
	public static class FormUpload
	{
		public const string HttpEncoding = "multipart/form-data";
		private static readonly Encoding Encoding = Encoding.UTF8;

		public static byte[] GetMultipartFormData(Dictionary<string, object> postParameters, string boundary)
		{
			Stream formDataStream = new System.IO.MemoryStream();
			var needsCrLf = false;

			foreach (var param in postParameters)
			{
				// Thanks to feedback from commenters, add a CRLF to allow multiple parameters to be added.
				// Skip it on the first parameter, add it to subsequent parameters.
				if (needsCrLf)
					formDataStream.Write(Encoding.GetBytes("\r\n"), 0, Encoding.GetByteCount("\r\n"));

				needsCrLf = true;

				if (param.Value is FileParameter)
				{
					var fileToUpload = (FileParameter)param.Value;

					// Add just the first part of this param, since we will write the file data directly to the Stream
					var header = string.Format("--{0}\r\nContent-Disposition: form-data; name=\"{1}\"; filename=\"{2}\";\r\nContent-Type: {3}\r\n\r\n",
						boundary,
						param.Key,
						fileToUpload.FileName ?? param.Key,
						fileToUpload.ContentType ?? "application/octet-stream");

					formDataStream.Write(Encoding.GetBytes(header), 0, Encoding.GetByteCount(header));

					// Write the file data directly to the Stream, rather than serializing it to a string.
					formDataStream.Write(fileToUpload.File, 0, fileToUpload.File.Length);
				}
				else
				{
					var postData = string.Format("--{0}\r\nContent-Disposition: form-data; name=\"{1}\"\r\n\r\n{2}",
						boundary,
						param.Key,
						param.Value);
					formDataStream.Write(Encoding.GetBytes(postData), 0, Encoding.GetByteCount(postData));
				}
			}

			// Add the end of the request.  Start with a newline
			var footer = "\r\n--" + boundary + "--\r\n";
			formDataStream.Write(Encoding.GetBytes(footer), 0, Encoding.GetByteCount(footer));

			// Dump the Stream into a byte[]
			formDataStream.Position = 0;
			var formData = new byte[formDataStream.Length];
			formDataStream.Read(formData, 0, formData.Length);
			formDataStream.Close();

			return formData;
		}

		public class FileParameter
		{
			public byte[] File { get; set; }
			public string FileName { get; set; }
			public string ContentType { get; set; }
			public FileParameter(byte[] file) : this(file, null) { }
			public FileParameter(byte[] file, string filename) : this(file, filename, null) { }
			public FileParameter(byte[] file, string filename, string contenttype)
			{
				File = file;
				FileName = filename;
				ContentType = contenttype;
			}
		}
	}
}
