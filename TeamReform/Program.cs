using System;
using System.IO;
using System.Text;
using System.Collections.Generic;

namespace TeamReform
{
    class Program
    {
        static void Main(string[] args)
        {
            TeamReform t = new TeamReform();
            List<List<int>> list = t.DevideBeforeTeam2After(12, 4, 16, true);

            // ファイルを書き込みモード（上書き）で開く
            StreamWriter file = new StreamWriter("team_list.csv", false, Encoding.UTF8);
            for (int i = 0; i < list.Count; i++)
            {
                String line = "";
                for (int j = 0; j < list[i].Count; j++)
                {
                    line += list[i][j] + ",";
                }
                // ファイルに書き込む
                file.WriteLine(line.Substring(0, line.Length - 1));
            }
            // ファイルを閉じる
            file.Close();
        }
    }
}
