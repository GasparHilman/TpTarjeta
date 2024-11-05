using Space;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP_Tarjeta_tests
{
    internal class Tarjeta_tests
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
        public void SaldosPosibles()
        {
            tarjeta.Cargar_tarjeta(2000);
            Assert.That(tarjeta.saldo, Is.EqualTo(2000));
            tarjeta.restar_saldo(2000);

            tarjeta.Cargar_tarjeta(3000);
            Assert.That(tarjeta.saldo, Is.EqualTo(3000));
            tarjeta.restar_saldo(3000);

            tarjeta.Cargar_tarjeta(4000);
            Assert.That(tarjeta.saldo, Is.EqualTo(4000));
            tarjeta.restar_saldo(4000);

            tarjeta.Cargar_tarjeta(5000);
            Assert.That(tarjeta.saldo, Is.EqualTo(5000));
            tarjeta.restar_saldo(5000);

            tarjeta.Cargar_tarjeta(6000);
            Assert.That(tarjeta.saldo, Is.EqualTo(6000));
            tarjeta.restar_saldo(6000);

            tarjeta.Cargar_tarjeta(7000);
            Assert.That(tarjeta.saldo, Is.EqualTo(7000));
            tarjeta.restar_saldo(7000);

            tarjeta.Cargar_tarjeta(8000);
            Assert.That(tarjeta.saldo, Is.EqualTo(8000));
            tarjeta.restar_saldo(8000);

            tarjeta.Cargar_tarjeta(9000);
            Assert.That(tarjeta.saldo, Is.EqualTo(9000));
        }

        [Test]
        public void CargarSaldoNoValido()
        {
            tarjeta.Cargar_tarjeta(2000);
            Assert.That(tarjeta.saldo, Is.EqualTo(2000));

            tarjeta.Cargar_tarjeta(0);
            Assert.That(tarjeta.saldo, Is.EqualTo(2000));

            tarjeta.Cargar_tarjeta(-500);
            Assert.That(tarjeta.saldo, Is.EqualTo(2000));
        }
    }
}

