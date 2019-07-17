using Xunit;
using TeamReform;
using System;
using System.Collections.Generic;

namespace TeamReform.UnitTests
{
    public class MemberConverter_Test
    {
        [Fact]
        public void ToMember()
        {
            Member mem = MemberConverter.ToMember(new List<String>(new String[] { "0", "AAA", "BB" }));
            Assert.Equal("0", mem.no);
            Assert.Equal("AAA", mem.team);
            Assert.Equal("BB", mem.name);
            Assert.Equal("", mem.befteam);
        }
        [Fact]
        public void ToInfo()
        {
            Member mem = new Member();
            mem.befteam = "BEFORE";
            mem.no = "10";
            mem.team = "AAA";
            mem.name = "NAME";

            List<String> list = MemberConverter.ToInfo(mem);
            Assert.Equal("AAA", list[0]);
            Assert.Equal("10", list[1]);
            Assert.Equal("BEFORE", list[2]);
            Assert.Equal("NAME", list[3]);
        }
    }
}