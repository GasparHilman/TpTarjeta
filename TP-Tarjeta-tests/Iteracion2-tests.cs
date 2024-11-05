using Space;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP_Tarjeta_tests
{
    internal class Iteracion2_tests
    {
        public TiempoFalso tiempo;
        public Colectivo k;
        public Tarjeta tarjeta;


        [SetUp]
        public void Setup()
        {
            tiempo = new TiempoFalso();
            tarjeta = new Tarjeta(1);
            k = new Colectivo("K");
      
        }
        [Test]
        public void PagarConSaldoPositivo()
        {
            tarjeta.Cargar_tarjeta(2000);
            k.PagarCon(tarjeta, tiempo);
            Assert.That(tarjeta.saldo, Is.EqualTo(2000 - k.precio));
            k.PagarCon(tarjeta, tiempo);
            Assert.That(tarjeta.saldo, Is.EqualTo(2000 - 2 * k.precio));
        }

        [Test]
        public void PagarSINSaldo()
        {
            k.PagarCon(tarjeta, tiempo);
            Assert.That(tarjeta.saldo, Is.EqualTo(0));

        }
        [Test]
        public void PagarConSaldoNegativo()
        {
            tarjeta.Cargar_tarjeta(2000);
            k.PagarCon(tarjeta, tiempo);
            Assert.That(tarjeta.saldo, Is.EqualTo(2000 - k.precio));
            k.PagarCon(tarjeta, tiempo);
            Assert.That(tarjeta.saldo, Is.EqualTo(2000 - 2 * k.precio));
            k.PagarCon(tarjeta, tiempo);
            Assert.That(tarjeta.saldo, Is.EqualTo(2000 - 2 * k.precio));
        }

    }
}
