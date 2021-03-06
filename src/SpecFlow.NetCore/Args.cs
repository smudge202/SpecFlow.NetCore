﻿using System;
using System.IO;
using System.Linq;
using static System.Console;

namespace Specflow.NetCore
{
	internal class Args
	{
		public const string SpecFlowPathArgName = "--specflow-path";
		public const string WorkingDirectoryArgName = "--working-directory";
		public const string TestFrameworkArgName = "--test-framework";
		public const string ToolsVersionArgName = "--tools-version";

		public string SpecFlowPath { get; }
		public DirectoryInfo WorkingDirectory { get; }
		public string TestFramework { get; }
		public string ToolsVersion { get; }

		public Args(string[] args)
		{
			WorkingDirectory = new DirectoryInfo(Directory.GetCurrentDirectory());

			if (args == null || !args.Any())
				return;

			// Very basic arg parsing:
			//   - Assume odd elements are arg names.
			//   - Assume even elements are arg values.
			//   - Paths with spaces will probably blow up.
			//   - Duplicates, last one wins.

			for (var i = 0; i < args.Length; i++)
			{
				if (IsOdd(i))
					continue;

				if (i + 1 >= args.Length)
					throw new Exception("Uneven arguments");

				var name = args[i];
				var value = args[i + 1];

				switch (name)
				{
					case SpecFlowPathArgName:
						SpecFlowPath = value;
						break;

					case WorkingDirectoryArgName:
						if (!Directory.Exists(value))
							throw new Exception("Working directory doesn't exist: " + value);
						WorkingDirectory = new DirectoryInfo(value);
						break;

					case TestFrameworkArgName:
						TestFramework = value;
						break;

					case ToolsVersionArgName:
						ToolsVersion = value;
						break;

					default:
						throw new Exception("Unknown argument: " + name);
				}
			}

			WriteLine("SpecFlowPath: " + SpecFlowPath);
			WriteLine("WorkingDirectory: " + WorkingDirectory.FullName);
			WriteLine("TestFramework: " + TestFramework);
		}

		private bool IsOdd(int i)
		{
			return i % 2 != 0;
		}
	}
}