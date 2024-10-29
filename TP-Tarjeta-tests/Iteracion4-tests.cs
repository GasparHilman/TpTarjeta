using NUnit.Framework;
using Space;


namespace TP_Tarjeta_tests
{
    public class Test_siteracion4
    {
        public TiempoFalso tiempo;
        public Colectivo k;
        public Tarjeta tarjeta;
        public Tarjeta tarjeta2;
        public Tarjeta medioBoleto;
        public Tarjeta gratuitoBoleto;
        public Colectivo expreso;

        [SetUp]
        public void Setup()
        {
            tiempo = new TiempoFalso();
            tarjeta = new Tarjeta(1);
            tarjeta2 = new Tarjeta(5);
            medioBoleto = new MedioBoleto(2);
            gratuitoBoleto = new GratuitoBoleto(3);
            k = new Colectivo("K");
            expreso = new ColectivoInterurbano("expreso");
        }

        [Test]
        public void uso_frecuente()
        {
            tarjeta.viajesmes = 28;
            tarjeta.Cargar_tarjeta(9000);
            k.PagarCon(tarjeta,tiempo); //boleto 29
            Assert.That(tarjeta.saldo, Is.EqualTo(9000-k.precio));
            Assert.That(tarjeta.viajesmes, Is.EqualTo(29));
            k.PagarCon(tarjeta, tiempo); //boleto 30
            Assert.That(tarjeta.saldo, Is.EqualTo(9000 - k.precio - ((int)(k.precio*0.8)) ));
            Assert.That(tarjeta.viajesmes, Is.EqualTo(30));
            tarjeta.viajesmes = 79;
            k.PagarCon(tarjeta, tiempo); //boleto 80
            Assert.That(tarjeta.saldo, Is.EqualTo(9000 - k.precio - ((int)(k.precio * 0.8)) - ((int)(k.precio * 0.75))));
            k.PagarCon(tarjeta, tiempo); //boleto 81
            Assert.That(tarjeta.saldo, Is.EqualTo(9000 - k.precio*2 - ((int)(k.precio * 0.8)) - ((int)(k.precio * 0.75))));
        }
        [Test]
        public void franja_horaria()
        {
            medioBoleto.Cargar_tarjeta(9000);
            gratuitoBoleto.Cargar_tarjeta(9000);
            k.PagarCon(medioBoleto, tiempo);
            k.PagarCon(gratuitoBoleto, tiempo);
            Console.WriteLine(tiempo.Now());
            Assert.That(medioBoleto.saldo, Is.EqualTo(gratuitoBoleto.saldo));
            Assert.That(medioBoleto.saldo, Is.EqualTo(9000-k.precio));
            tiempo.AgregarHoras(7);
            Console.WriteLine(tiempo.Now());
            k.PagarCon(medioBoleto, tiempo);
            k.PagarCon(gratuitoBoleto, tiempo);
            Assert.That(medioBoleto.saldo, Is.EqualTo(9000-k.precio-k.precio/2)); //saldo restante = 9000-k.precio
            Assert.That(gratuitoBoleto.saldo, Is.EqualTo(9000-k.precio));
        }

        [Test]
        public void interurbanas()
        {
            tarjeta.Cargar_tarjeta(9000);
            tarjeta2.Cargar_tarjeta(9000);
            expreso.PagarCon(tarjeta, tiempo);
            k.PagarCon(tarjeta2, tiempo);
            Assert.That(tarjeta.saldo, Is.LessThan(tarjeta2.saldo));
        }

    }
}