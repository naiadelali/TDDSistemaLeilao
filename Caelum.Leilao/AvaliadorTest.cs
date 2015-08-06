using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caelum.Leilao
{
    [TestClass]
    public class AvaliadorTest
    {

        private Avaliador leiloeiro;
        private Usuario maria;
        private Usuario jose;
        private Usuario joao;

        [TestInitialize]
        public void SetUp()
        {
            this.leiloeiro = new Avaliador();
            this.joao = new Usuario("João");
            this.jose = new Usuario("José");
            this.maria = new Usuario("Maria");
        }

        [TestMethod]
        public void DeveEntenderLancesEmOrdemCrescente()
        {

            Leilao leilao = new CriadorDeLeilao()
                .Para("Playstation 3 Novo")
                .Lance(joao, 250)
                .Lance(jose, 300)
                .Lance(maria, 400)
                .Constroi();

            leiloeiro.Avalia(leilao);

            Assert.AreEqual(400.0, leiloeiro.MaiorLance, 0.00001);
            Assert.AreEqual(250.0, leiloeiro.MenorLance, 0.00001);
        }

        [TestMethod]
        public void DeveEntenderLeilaoComApenasUmLance()
        {
            Leilao leilao = new CriadorDeLeilao()
            .Para("Playstation 3 Novo")
            .Lance(joao, 1000)
            .Constroi();

            leiloeiro.Avalia(leilao);

            Assert.AreEqual(1000.0, leiloeiro.MaiorLance, 0.00001);
            Assert.AreEqual(1000.0, leiloeiro.MenorLance, 0.00001);
        }

        [TestMethod]
        public void DeveEncontrarOsTresMaioresLances()
        {
            Leilao leilao = new CriadorDeLeilao()
                .Para("Playstation 3 Novo")
                .Lance(joao, 100)
                .Lance(maria, 200)
                .Lance(joao, 300)
                .Lance(maria, 400)
                .Constroi();

            leiloeiro.Avalia(leilao);

            var maiores = leiloeiro.TresMaiores;
            Assert.AreEqual(3, maiores.Count);
            Assert.AreEqual(400.0, maiores[0].Valor, 0.00001);
            Assert.AreEqual(300.0, maiores[1].Valor, 0.00001);
            Assert.AreEqual(200.0, maiores[2].Valor, 0.00001);
        }

        [TestMethod, ExpectedException(typeof(Exception))]
        public void NaoDeveAvaliarLeiloesSemNenhumLanceDado()
        {
            Leilao leilao = new CriadorDeLeilao()
                .Para("Playstation 3 Novo")
                .Constroi();

            leiloeiro.Avalia(leilao);
        }
    }
}
