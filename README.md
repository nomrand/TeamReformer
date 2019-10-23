[Japanese version of readme is here](./README.ja.md)


# Team Reforming Tool
Reform the "Xt" Teams (Each team has "Xm" members) to new "Yt" teams (Each team has "(Xt*Xm)/Yt" members).

And, avoid that reformed after-teams have same before-team members.


## How to use
### If execute "exe" file
```
 TeamReform.exe <Num_of_Teams_after_reformed> [Directory_Path_of_InOut_File *Optional]
```
1. Put the following file in "Directory_Path_of_InOut_File" (if omitted, it will be the same directory of "exe").
* base_list.csv
  * Line 1 is a header (ignored)
  * Each lines 2- are information of members which is not reformed
    * Column 0:Member's element 1
    * Column 1:Member's before-team (which is used to team-reforming)
    * Column 2:Member's element 2

2. Execute "exe" file.

3. The tool makes following file (into same directory of "base_list.csv")
* team_list.csv
  * Line 1 is a header
  * Each lines 2- are information of members which is already reformed
    * Column 0:Member's after-team (=reformed teams. it must be "0", "1", "2", ...)
    * Column 1:Member's element 1
    * Column 2:Member's before-team
    * Column 3:Member's element 2

### If want to use in Unity
1. Copy "TeamReform\TeamReform.cs" into Unity's compile target directory (such as "Asset\Scripts").

2. In other CS script, you can use "TeamReform" utility method.
```
 var beforeTeamMemberList = new List<List<String>>();
 int whichColumIsTeamName = 1;
 int howManyAfterTeams = 12;
 // ... do something
 var resultTeamMemberList = TeamReform.TeamReform.ReformTeam(beforeTeamMemberList, whichColumIsTeamName, howManyAfterTeams);
```

* You can see the details of how-to-use the method, in "TeamReform\Program.cs" or comments of "TeamReform\TeamReform.cs".


## Limitations
* Team names of reformed after-teams (Column 0 of "team_list.csv") are 数字文字("0", "1", "2", ...)となる
* If Xt <= Yt (it means reformed after-teams are more than before-teams), each reformed after-team don't have duplicated before-teams teammembers.
* If Xt > Yt (it means reformed after-teams are less than before-teams), each reformed after-team may have duplicated before-teams teammembers.

## Algorithm
This tool works like ... each before-teams member choose after-teams randomly.
1. Shuffle the order of before-teams (such as TEAM1, TEAM2, TEAM3 -> TEAM2, TEAM1, TEAM3)
2. Shuffle the order of members in before-teams (such as Mr.A in TEAM1, Mr.B in TEAM1, Mr.C in TEAM1 -> Mr.C in TEAM1, Mr.B in TEAM1, Mr.A in TEAM1)

* In the shuffles above, change the order only. Not change the team of each member

3. Each shuffled member of shuffled before-teams, choose after-teams randomly (such as Mr.A in TEAM1 chooses AFTERTEAM1, Mr.B chooses AFTERTEAM2, ...)
  * Cannot choose the after-teams which already chosen by other members
  * If all after-teams are chosen by other members, can choose all after-teams 
4. Repeat above, until all members of all after-teams finish choosing
