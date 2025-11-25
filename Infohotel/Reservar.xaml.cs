using Infohotel.Models;

namespace Infohotel;

public partial class Reservar : ContentPage
{
    public List<Room> Rooms { get; set; }

    public Reservar()
    {
        InitializeComponent();

        Rooms = new List<Room>
        {
            new Room
            {
                Id = "1",
                Name = "Bahia View 1 King Room",
                ShortDescription = "Habitación king con vistas al bosque y baño estilo spa.",
                Description = "Sumérjase en las relajantes vistas del bosque tropical desde la bañera independiente en el balcón privado amueblado o relájese en la comodidad de la lujosa cama tamaño king, disfrutando de la serena vista a través de los amplios ventanales del piso al techo. Estas espaciosas habitaciones son una combinación armoniosa de decoración elegante y comodidad moderna, con tonos naturales relajantes y diseños orgánicos que crean una atmósfera serena y relajante. El baño ofrece una experiencia similar a la de un spa, con una ducha de lluvia de gran tamaño con vista al balcón, tocadores dobles con generoso espacio en el mostrador y un inodoro separado para mayor privacidad.\r\n\r\nLas comodidades adicionales incluyen una cama tamaño king bien equipada, un escritorio elegante, un televisor de alta definición de 65 pulgadas, batas de felpa, amplio espacio de almacenamiento y una caja fuerte en la habitación.",
                ImageUrl = "kingroom.jpg",
                MaxGuests = 3,
                PricePerNight = 1349
            },
            new Room
            {
                Id = "2",
                Name = "Bahia View 2 Queens Room",
                ShortDescription = "Dos camas queen con vistas tropicales y baño tipo spa.",
                Description = "Disfrute de las vistas tropicales desde la bañera independiente en el balcón privado amueblado. Alternativamente, disfrute de la vista desde los amplios ventanales del piso al techo de estas espaciosas habitaciones, que ofrecen una decoración elegante y comodidad. Tonos naturales relajantes y diseños orgánicos crean una atmósfera relajante. El baño cuenta con una ducha de lluvia de gran tamaño con vista al balcón, grandes tocadores dobles con generoso espacio en el mostrador y un inodoro separado.\r\nLas comodidades adicionales incluyen: dos camas tamaño queen bien equipadas, escritorio, televisor de alta definición de 65 pulgadas, túnicas lujosas, amplio espacio de almacenamiento y una caja fuerte en la habitación.",
                ImageUrl = "queensroom.jpg",
                MaxGuests = 4,
                PricePerNight = 1484
            },
            new Room
            {
                Id = "3",
                Name = "Bahia View 2 Queens Room - Plunge Pool",
                ShortDescription = "Habitación con dos queen y piscina privada exterior.",
                Description = "Rodéese de exuberantes vistas tropicales desde la relajante piscina del patio. Alternativamente, disfrute de la impresionante vista a través de los amplios ventanales de piso a techo en estas espaciosas habitaciones, que ofrecen una decoración elegante y comodidad. Tonos naturales relajantes y diseños orgánicos crean una atmósfera serena y relajante. El baño cuenta con una ducha de lluvia de gran tamaño con vistas al balcón, grandes tocadores dobles con amplio espacio en el mostrador y un inodoro separado.\r\n\r\nLas comodidades adicionales incluyen: dos camas tamaño queen bien equipadas, escritorio, televisor de alta definición de 65 pulgadas, túnicas lujosas, amplio espacio de almacenamiento y una caja fuerte en la habitación.",
                ImageUrl = "queensroomplungepool.jpg",
                MaxGuests = 4,
                PricePerNight = 1551
            },
            new Room
            {
                Id = "4",
                Name = "Ocean View 1 King Room - Large Balcony",
                ShortDescription = "King con gran balcón envolvente y vistas espectaculares al mar.",
                Description = "Impresionantes vistas al mar se despliegan desde el balcón amueblado envolvente de gran tamaño, completo con una bañera independiente para una relajación máxima. Su codiciada ubicación en esquina cuenta con ventanas de piso a techo, lo que le permite disfrutar del impresionante paisaje desde la comodidad de su cama. Diseñadas para la serenidad, estas espaciosas habitaciones combinan una decoración elegante con tonos naturales relajantes y texturas orgánicas, creando un refugio tranquilo. El baño inspirado en el spa cuenta con una ducha de lluvia de gran tamaño con vista al balcón, tocadores dobles con generoso espacio en el mostrador y un inodoro separado. Las comodidades adicionales incluyen una cama tamaño king bien equipada, un espacio de trabajo, un televisor de alta definición de 65 pulgadas, batas de felpa, amplio espacio de almacenamiento y una caja fuerte en la habitación.",
                ImageUrl = "kingroomlargebalcony.jpg",
                MaxGuests = 4,
                PricePerNight = 2399
            },
            new Room
            {
                Id = "5",
                Name = "Ocean View 2 Queens Room",
                ShortDescription = "Dos queen con vista oceánica y baño lujoso.",
                Description ="Impresionantes vistas del resplandeciente océano azul definen esta espaciosa y elegantemente diseñada habitación de invitados. Disfrute de la fascinante vista desde la bañera independiente en el balcón privado amueblado o desde las amplias ventanas de piso a techo que conectan perfectamente el interior con la naturaleza. La elegante decoración de la habitación, realzada por tonos naturales relajantes y elementos orgánicos, crea una atmósfera serena y acogedora. El lujoso baño está diseñado para la máxima relajación y cuenta con una ducha de lluvia de gran tamaño con vista al balcón, tocadores dobles con generoso espacio en el mostrador y un inodoro separado para mayor privacidad y comodidad.\r\n\r\nOtras comodidades incluyen: dos camas tamaño queen bien equipadas, un escritorio, un televisor de alta definición de 65 pulgadas, túnicas lujosas, amplias áreas de almacenamiento y una caja fuerte en la habitación.",
                ImageUrl = "oceanviewqueensroom.jpg",
                MaxGuests = 4,
                PricePerNight = 2514
            },
            new Room
            {
                Id = "6",
                Name = "2 Qn Ocnview Mobility/hearing Access Ri Shwr",
                ShortDescription = "Habitación accesible con dos queen y ducha enrollable.",
                Description ="Esta habitación estándar accesible para personas con movilidad y audición cuenta con dos camas queen y una ducha enrollable. La sala también incluye una alarma visual y dispositivos de notificación para el timbre, el toque de puerta y las llamadas telefónicas entrantes.\r\n\r\nImpresionantes vistas azules al mar definen esta espaciosa y moderna habitación de invitados. Disfrute de la vista desde la bañera independiente en el balcón privado amueblado o desde los amplios ventanales del piso al techo. Una decoración elegante, con tonos naturales relajantes y diseños orgánicos, crea una atmósfera relajante. El baño ofrece una ducha de lluvia de gran tamaño con vista al balcón, grandes tocadores dobles con amplio espacio en el mostrador y un inodoro separado.\r\n\r\nLas comodidades adicionales incluyen: dos camas tamaño queen bien equipadas, escritorio, televisor de alta definición de 65 pulgadas, túnicas lujosas, amplio espacio de almacenamiento y una caja fuerte en la habitación.\r\n\r\nEs posible que cualquier fotografía correspondiente no refleje el tipo de habitación accesible o la característica específica de la habitación.",
                ImageUrl = "qn.jpg",
                MaxGuests = 4,
                PricePerNight = 2514
            },
            new Room
            {
                Id = "7",
                Name = "Ocean 1 King 1 Bedroom Suite - Plunge Pool",
                ShortDescription = "Suite con piscina privada, sala amplia y vista al mar.",
                Description ="Disfrute de la tranquilidad de las infinitas aguas azules desde esta suite de elegante diseño, donde una piscina privada realza el amplio balcón amueblado. Los ventanales de piso a techo bañan el espacio con luz natural, combinando a la perfección la belleza del exterior con interiores refinados. La sala de estar está cuidadosamente organizada con un lujoso sofá y dos sillones, que fluyen sin esfuerzo hacia un sofisticado comedor con capacidad para ocho personas. Un refugio sereno, el dormitorio cuenta con una cama king bien equipada, una acogedora zona de estar y acceso directo al balcón privado para momentos ininterrumpidos de relajación. El baño inspirado en el spa cuenta con una ducha de lluvia de gran tamaño con vista a la terraza, complementada con una ducha de mano, tocadores dobles con generoso espacio en el mostrador y un inodoro separado.Un discreto medio baño está convenientemente ubicado junto a la entrada para mayor facilidad.\r\n\r\nLas comodidades adicionales incluyen un televisor de alta definición de 65 pulgadas, batas de felpa, amplio armario y espacio de almacenamiento y una caja fuerte en la habitación.",
                ImageUrl = "suiteoceankingsroom.jpg",
                MaxGuests = 3,
                PricePerNight = 2863
            },
            new Room
            {
                Id = "8",
                Name = "Ocean View 1 King Room",
                ShortDescription = "King moderna con vistas al océano y baño tipo spa.",
                Description ="Sumérgete en impresionantes vistas de las brillantes aguas azules desde esta amplia y moderna habitación de invitados. Ya sea desde la bañera independiente en el balcón privado amueblado o la lujosa cama tamaño king ubicada frente a las ventanas del piso al techo, cada ángulo ofrece una vista panorámica serena. La elegante decoración de la habitación combina armoniosamente tonos naturales relajantes con diseños orgánicos, cultivando una atmósfera de tranquilidad y elegancia refinada. El baño inspirado en el spa cuenta con una ducha de lluvia de gran tamaño con vistas al balcón, tocadores dobles con generoso espacio en el mostrador y un inodoro separado para mayor privacidad.\r\n\r\nLas comodidades adicionales incluyen una cama tamaño king bien equipada, un escritorio elegante, un televisor de alta definición de 65 pulgadas, batas de felpa, amplio espacio de almacenamiento y una caja fuerte en la habitación.",
                ImageUrl = "oceanviewkingroom.jpg",
                MaxGuests = 3,
                PricePerNight = 1563
            }
        };

        BindingContext = this;
    }

    private async void OnViewRoomClicked(object sender, EventArgs e)
    {
        var button = (Button)sender;
        var room = (Room)button.BindingContext;

        // Ir a la pantalla de detalle de ESA habitación
        await Navigation.PushAsync(new HabitacionDetalle(room));
    }
}
