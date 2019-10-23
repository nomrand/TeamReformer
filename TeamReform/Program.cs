/// Copyright [2019] [https://github.com/nomrand]
/// 
/// Licensed under the Apache License, Version 2.0 (the "License");
/// you may not use this file except in compliance with the License.
/// You may obtain a copy of the License at
/// 
///     http://www.apache.org/licenses/LICENSE-2.0
/// 
/// Unless required by applicable law or agreed to in writing, software
/// distributed under the License is distributed on an "AS IS" BASIS,
/// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
/// See the License for the specific language governing permissions and
/// limitations under the License.
using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace TeamReform
{
    class Program
    {
        public const String BEFORE_CSV_NAME = "base_list.csv";
        public const String AFTER_CSV_NAME = "team_list.csv";

        static void Main(string[] args)
        {
            int afterTeamNum = 0;
            String filedir = Directory.GetParent(Assembly.GetExecutingAssembly().Location).FullName;
            // GET ARGS
            if (args.Length == 0 || args.Length > 2)
            {
                // Illigal Arguments
                Console.WriteLine("Usage: <ProgramName> <Number of After Team> [input directory path(option)]");
                return;
            }
            if (args.Length == 2 && args[0] == "-s")
            {
                // Score Mode
                Console.WriteLine("### SCORE MODE ###");
                scoreCSV(args[1]);
                return;
            }

            Console.WriteLine($"### SHUFFLE MODE ###");
            // Number of After Team
            afterTeamNum = int.Parse(args[0]);
            if (args.Length > 1)
            {
                // input file path
                filedir = args[1];
            }
            if (!filedir.EndsWith(System.IO.Path.DirectorySeparatorChar))
            {
                filedir += System.IO.Path.DirectorySeparatorChar;
            }

            // READ
            var beforeTeamMembers = readCSV(filedir + BEFORE_CSV_NAME);
            // slice body (no header)
            beforeTeamMembers = beforeTeamMembers.GetRange(1, beforeTeamMembers.Count - 1);

            // CONVERT
            Console.WriteLine($"# Convert {beforeTeamMembers.Count} members to {afterTeamNum} teams");
            var afterTeamMembers = TeamReform.ReformTeam(beforeTeamMembers, 1, afterTeamNum);
            Console.WriteLine($"# Score (lower is better-shuffled):{TeamReform.ReformScore(afterTeamMembers, 0, 2, true)}");

            // WRITE
            // add headr
            afterTeamMembers.Insert(0, new String[] { "TEAM", "NO", "SCHOOL", "NAME" }.ToList());
            writeCSV(filedir + AFTER_CSV_NAME, afterTeamMembers);
        }

        static public List<List<String>> readCSV(String filepath)
        {
            var result = new List<List<String>>();
            try
            {
                // Open with read only
                using (FileStream stream = new FileStream(filepath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                using (StreamReader file = new StreamReader(stream, Encoding.UTF8))
                {
                    while (!file.EndOfStream)
                    {
                        string line = file.ReadLine();
                        result.Add(new List<string>(line.Split(',')));
                    }
                }
            }
            catch (IOException ioEx)
            {
                // if error happened, just throw
                throw ioEx;
            }
            return result;
        }

        static public void writeCSV(String filepath, List<List<String>> data)
        {
            try
            {
                // Open
                using (StreamWriter file = new StreamWriter(filepath, false, Encoding.UTF8))
                {
                    for (int i = 0; i < data.Count; i++)
                    {
                        String line = "";
                        for (int j = 0; j < data[i].Count; j++)
                        {
                            line += data[i][j] + ",";
                        }
                        file.WriteLine(line.Substring(0, line.Length - 1));
                    }
                }
            }
            catch (IOException ioEx)
            {
                // if error happened, just throw
                throw ioEx;
            }
        }

        static public void scoreCSV(String filepath)
        {
            try
            {
                // READ
                var teamMembers = readCSV(filepath);
                // slice body (no header)
                teamMembers = teamMembers.GetRange(1, teamMembers.Count - 1);

                Console.WriteLine($"# Score (lower is better-shuffled):{TeamReform.ReformScore(teamMembers, 0, 2, true)}");
            }
            catch (IOException ioEx)
            {
                Console.WriteLine($"Can't get Score by file({filepath})", ioEx);
            }
        }
    }
}
