using System;
using System.Collections.Generic;
using System.Linq;

namespace TeamReform
{
    public class MemberConverter
    {
        static public Member ReformMember(Member mem, String team)
        {
            Member aftmem = new Member();
            aftmem.befteam = mem.team;
            aftmem.team = team;
            aftmem.name = mem.name;
            aftmem.no = mem.no;

            return aftmem;
        }

        static public Member ToMember(List<String> memberInfo)
        {
            Member mem = new Member();

            mem.no = memberInfo[0];
            mem.team = memberInfo[1];
            mem.name = memberInfo[2];

            return mem;
        }

        static public List<String> ToInfo(Member mem)
        {
            List<String> memberInfo = new List<String>();

            memberInfo.Add(mem.team);
            memberInfo.Add(mem.no);
            memberInfo.Add(mem.befteam);
            memberInfo.Add(mem.name);

            return memberInfo;
        }
    }
}
