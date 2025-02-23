﻿using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace Debugger
{
	internal class Program
	{
		const int DEFAULT_PORT = 4711;

		private static bool trace_requests;
		private static bool trace_responses;
		static string LOG_FILE_PATH = null;

		private static async Task Main(string[] argv)
		{
			int port = -1;

			// parse command line arguments
			foreach (var a in argv) {
				switch (a) {
				case "--trace":
					trace_requests = true;
					break;
				case "--trace=response":
					trace_requests = true;
					trace_responses = true;
					break;
				case "--server":
					port = DEFAULT_PORT;
					break;
				default:
					if (a.StartsWith("--server=")) {
						if (!int.TryParse(a.Substring("--server=".Length), out port)) {
							port = DEFAULT_PORT;
						}
					}
					else if( a.StartsWith("--log-file=")) {
						LOG_FILE_PATH = a.Substring("--log-file=".Length);
					}
					break;
				}
			}

			if (string.IsNullOrEmpty(Environment.GetEnvironmentVariable("debug_logfile")) == false) {
				LOG_FILE_PATH = Environment.GetEnvironmentVariable("debug_logfile");
				trace_requests = true;
				trace_responses = true;
			}

			if (port > 0) {
				// TCP/IP server
				Program.Log("waiting for debug protocol on port " + port);
				await RunServer(port);
			} else {
				// stdin/stdout
				Program.Log("waiting for debug protocol on stdin/stdout");
				await RunSession(Console.OpenStandardInput(), Console.OpenStandardOutput());
			}
		}

		static TextWriter logFile;

		public static void Log(bool predicate, string format, params object[] data)
		{
			if (predicate)
			{
				Log(format, data);
			}
		}
		
		public static void Log(string format, params object[] data)
		{
			try
			{
				Console.Error.WriteLine(format, data);

				if (LOG_FILE_PATH != null)
				{
					if (logFile == null)
					{
						logFile = File.CreateText(LOG_FILE_PATH);
					}

					string msg = string.Format(format, data);
					logFile.WriteLine(string.Format("{0} {1}", DateTime.UtcNow.ToLongTimeString(), msg));
				}
			}
			catch (Exception ex)
			{
				if (LOG_FILE_PATH != null)
				{
					try
					{
						File.WriteAllText(LOG_FILE_PATH + ".err", ex.ToString());
					}
					catch
					{
					}
				}

				throw;
			}
		}

		private static async Task RunSession(Stream inputStream, Stream outputStream) {
			DebugSession debugSession = new DebugSession();
			debugSession.TRACE = trace_requests;
			debugSession.TRACE_RESPONSE = trace_responses;
			await debugSession.Start(inputStream, outputStream);

			if (logFile != null) {
				logFile.Flush();
				logFile.Close();
				logFile = null;
			}
		}
		private static async Task RunServer(int port) {
			var serverSocket = new TcpListener(IPAddress.Parse("127.0.0.1"), port);
			serverSocket.Start();

			while (true) {
				var clientSocket = await serverSocket.AcceptSocketAsync();
				if (clientSocket != null) {
					Program.Log(">> accepted connection from client");

					using (var networkStream = new NetworkStream(clientSocket)) {
						try {
							await RunSession(networkStream, networkStream);
						} catch (Exception e) {
							Console.Error.WriteLine("Exception: " + e);
						}
					}

					clientSocket.Close();
					Program.Log(">> client connection closed");
				}
			}
		}
	}
}
