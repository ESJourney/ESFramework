using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProfundizandoCSharpTests
{
    public class ProbandoTiposPrimitivos
    {
        [Fact]
        public void DividirDosNúmerosEnterosResultaEnUnNumeroEnteroTruncado()
        {
            
            var result = 20180514 / 10000;
            Assert.Equal(2018, result);

        }

        [Fact]
        public void Tests()
        {
            var birthdate = 20180514;
            var result = (birthdate / 10000 + 6) * 10000 + 9 * 100 + 1;
            Assert.Equal(20240901, result);
        }
    }
}
