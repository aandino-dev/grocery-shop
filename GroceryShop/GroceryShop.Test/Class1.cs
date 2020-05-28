using System;
using Xunit;

namespace GroceryShop.Test
{
    public class Class1
    {
        [Fact]
        public void ThisShouldPass()
        {
            var expected = 1;
            var actual = 1;

            Assert.Equal(expected, actual);
        }
    }
}
