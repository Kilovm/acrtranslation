using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WiiUtil
{
    class CommandHandler
    {
        public string[] Options { get; private set;  }
        public string[] Args { get; private set; }

        public bool hasOption(String option)
        {
            foreach (String opt in Options)
            {
                if (opt == option)
                    return true;
            }
            return false;
        }

        public CommandHandler(string[] args, bool console)
		{
            int start = console ? 0 : 1;
            List<String> options = new List<String>();
            List<String> realArgs = new List<String>();
            bool start_real_args = false;
            for (int i = start; i < args.Length; ++i)
            {
                if (!start_real_args)
                {
                    if (args[i].StartsWith("--"))
                    {
                        options.Add(args[i]);
                    }
                    else if (args[i].StartsWith("-"))
                    {
                        for (int j = 1; j < args[i].Length; ++j)
                        {
                            options.Add(new String(args[i].ElementAt(j), 1));
                        }
                    }
                    else
                    {
                        start_real_args = true;
                        realArgs.Add(args[i]);
                    }
                }
                else
                {
                    realArgs.Add(args[i]);
                }
            }
            Options = options.ToArray();
            Args = realArgs.ToArray();
		}
    }
}
