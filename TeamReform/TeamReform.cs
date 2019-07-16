using System;
using System.Collections.Generic;
using System.Linq;

namespace TeamReform
{
    public class TeamReform
    {
        Random r = new Random();

        /// <summary>
        /// create deviced Team list
        /// </summary>
        /// <param name="befTeamNum">number of Team before devide</param>
        /// <param name="befMemPerTeamNum">number of Members in Team before devide</param>
        /// <param name="aftTeamNum">number of Team after devide</param>
        /// <param name="isRandom">whether shuffle result or not</param>
        /// <returns>devided Team list</returns>
        public List<List<int>> DevideBeforeTeam2After(int befTeamNum, int befMemPerTeamNum, int aftTeamNum, bool isShuffle)
        {
            // init
            List<List<int>> result = new List<List<int>>();
            for (int i = 0; i < aftTeamNum; i++)
            {
                result.Add(new List<int>());
            }
            // init for random
            List<int> randomSelectFromBefTeam = Enumerable.Range(0, befTeamNum).ToList();
            if (isShuffle)
            {
                randomSelectFromBefTeam = randomSelectFromBefTeam.OrderBy(a => Guid.NewGuid()).ToList();
            }
            // toArray is shuffled at following step
            List<int> randomSelectToAftTeamOrg = Enumerable.Range(0, aftTeamNum).ToList();
            List<int> randomSelectToAftTeam = new List<int>();

            // first, devide befTeam to aftTeam
            foreach (int befTeam in randomSelectFromBefTeam)
            {
                for (int mem = 0; mem < befMemPerTeamNum; mem++)
                {
                    // Order of devided after Team change at every loop
                    if (randomSelectToAftTeam.Count == 0)
                    {
                        randomSelectToAftTeam.AddRange(randomSelectToAftTeamOrg);
                        if (isShuffle)
                        {
                            randomSelectToAftTeam = randomSelectToAftTeam.OrderBy(a => Guid.NewGuid()).ToList();
                        }
                    }

                    // set before Team, to after Team
                    result[randomSelectToAftTeam[0]].Add(befTeam);
                    randomSelectToAftTeam.RemoveAt(0);
                }
            }

            if (isShuffle)
            {
                // at last, shuffle list
                result = result.OrderBy(a => Guid.NewGuid()).ToList();
            }
            return result;
        }
    }
}
