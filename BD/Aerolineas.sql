Use master
Go 

if exists(Select * from sysdatabases where name = 'AeropuertosAmericanos')
begin
	drop database AeropuertosAmericanos
end
go

create database AeropuertosAmericanos
go

CREATE LOGIN [IIS APPPOOL\DefaultAppPool] FROM WINDOWS 
GO

use AeropuertosAmericanos
go

CREATE USER [IIS APPPOOL\DefaultAppPool] FOR LOGIN [IIS APPPOOL\DefaultAppPool]
GO

exec sys.sp_addrolemember 'db_owner', [IIS APPPOOL\DefaultAppPool]
go


--Tablas

create Table Clientes
(
	nroPasaporte varchar(7) primary key check (nroPasaporte like '[A-Z][0-9][0-9][0-9][0-9][0-9][0-9]'),
	nombre varchar(20) not null,
	contrasenia varchar(20) not null,
	nroTarjeta bigint not null,
	activo bit not null default(1)
)
go

create table Empleados
(
	usuario varchar(20) primary key not null,
	nombre varchar(20) not null,
	contrasenia varchar(20) not null,
	cargo varchar(8) not null check(cargo in ('Gerente','Vendedor','Admin'))
)
go

create table Ciudades
(
	codigoC varchar(6) primary key check(codigoC like '[A-Z][A-Z][A-Z][A-Z][A-Z][A-Z]'),
	ciudad varchar(30) not null,
	pais varchar(30) not null,
	activo bit not null default(1)
)
go

create table Aeropuertos
(
	codigoA varchar(3) primary key check(codigoA like '[A-Z][A-Z][A-Z]'),
	nombreA varchar(20) not null,
	direccion varchar(50) not null,
	impuestoPartida float not null check(impuestoPartida >= 0),
	impuestoLlegada float not null check(impuestoLlegada >= 0),
	codigoC varchar(6) not null references Ciudades(codigoC),
	activo bit not null default(1)
)
go

create table Vuelos
(
	codigoV varchar(15) primary key check(codigov like '[0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][A-Z][A-Z][A-Z]'),
	fechaD datetime not null check(fechaD > getdate()),
	fechaA datetime not null,
	precio float not null check(precio > 0),
	cantAsientos int not null check (cantAsientos between 100 and 300),
	codigoA varchar(3) not null references Aeropuertos(codigoA),
	codigoB varchar(3) not null references Aeropuertos(codigoA),
	check(fechaA > fechaD)
)
go

create table Venta
(
	nroTicket int identity(1,1) primary key,
	monto float not null check(monto > 0),
	fechaCompra DateTime not null default(getdate()),
	nroPasaporte varchar(7) not null references Clientes(nroPasaporte),
	usuario varchar(20) not null references Empleados(usuario),
	codigoV varchar(15) not null references Vuelos(codigoV)
)
go

create table Adquieren
(
	nroPasaporte varchar(7) not null references Clientes(nroPasaporte),
	nroTicket int not null references Venta(nroTicket),
	nroAsiento int not null check(nroAsiento > 0),
	primary key (nroPasaporte,nroTicket)
)
go

--Datos de Prueba--

Insert Clientes(nroPasaporte,contrasenia,nombre,nrotarjeta) values
('d702157','asd123','Rodrigo Amuz',			123456789012346),
('a135431','zxc123','Nicolas Amuz',			123456780123465),
('b135132','123zcx','Marcos Santa Marta',	123456783465901),
('c123165','123asd','Santiago Santa Marta', 123490123465567),
('e143546','qwe123','Facundo Silva',		567890123465123),
('c465464','123qwe','Diego Fernandez',		901212345678346),
('a123456','asd076','Julio Montero',123456789012297),
('a123457','asd077','Julio Morales',123456789012298),
('a123458','asd078','Atilio Garcia',123456789012299),
('a123459','asd079','Oscar Morales',123456789012300),
('a123460','asd080','Jose Vanzino',123456789012301),
('a123461','asd081','Alfredo Foglino',123456789012302),
('a123462','asd082','Angel Romano',123456789012303),
('a123463','asd083','Hector Scarone',123456789012304),
('a123464','asd084','Vicrtor Esparrago',123456789012305),
('a123465','asd085','Juan Blanco',123456789012306),
('a123466','asd086','Roberto Sosa',123456789012307),
('a123467','asd087','Jose Pintos',123456789012308),
('a123468','asd088','Rodolfo Rodriguez',123456789012309),
('a123469','asd089','Anibal Ciocca',123456789012310),
('a123470','asd090','Hailton Correia',123456789012311),
('a123471','asd091','Juan Carrasco',123456789012312),
('a123472','asd092','Shubert Gambetta',123456789012313),
('a123473','asd093','Pascual Somma',123456789012314),
('a123474','asd094','Ildo Maneiro',123456789012315),
('a123475','asd095','Santos Urdinaran',123456789012316),
('a123476','asd096','Fabian Cohelo',123456789012317),
('a123477','asd097','Jorge Sere',123456789012318),
('a123478','asd098','Jorge Cardaccio',123456789012319),
('a123479','asd099','Juan Mameli',123456789012320),
('a123480','asd100','Yubert Lemos',123456789012321),
('a123481','asd101','Roberto Porta',123456789012322),
('a123482','asd102','Jose Santamaria',123456789012323),
('a123483','asd103','Tony Gomez',123456789012324),
('a123484','asd104','Jose Moreira',123456789012325),
('a123485','asd105','Felipe Revelez',123456789012326),
('a123486','asd106','EUgenio Galvalisi',123456789012327),
('a123487','asd107','Luis Cruz',123456789012328),
('a123488','asd108','Guillermo Esacalada',123456789012329),
('a123489','asd109','Luis Castro',123456789012330),
('a123490','asd110','Andres Mazali',123456789012331),
('a123491','asd111','Alberto Bica',123456789012332),
('a123492','asd112','Gonzalo Castro',123456789012333),
('a123493','asd113','Luis Ubiña',123456789012334),
('a123494','asd114','Diego Arismendi',123456789012335),
('a123495','asd115','Juan Cabrera',123456789012336),
('a123496','asd116','Juan Mugica',123456789012337),
('a123497','asd117','Raul Pini',123456789012338),
('a123498','asd118','Arsenio Luzardo',123456789012339),
('a123499','asd119','Eduardo Garcia',123456789012340),
('a123500','asd120','Alejandro Lembo',123456789012341),
('a123501','asd121','Luis Cubilla',123456789012342),
('a123502','asd122','Juan Romero',123456789012343),
('a123503','asd123','Gustavo Munua',123456789012344),
('a123504','asd124','Sebastian Fernandez',123456789012345),
('a123505','asd125','Julio Dely Valdes',123456789012346),
('a123506','asd126','Gustavo Varela',123456789012347),
('a123507','asd127','Santiago Demarchi',123456789012348),
('a123508','asd128','Gustavo Mendez',123456789012349),
('a123509','asd129','Hector Castro',123456789012350),
('a123510','asd130','Marco Vanzini',123456789012351),
('a123511','asd131','Horacio Troche',123456789012352),
('a123512','asd132','Ignacio Prieto',123456789012353),
('a123513','asd133','Adan Machado',123456789012354),
('a123514','asd134','Carlos Scarone',123456789012355),
('a123515','asd135','Angel Brunel',123456789012356),
('a123516','asd136','Alfredo Zibechi',123456789012357),
('a123517','asd137','Atilio Ancheta',123456789012358),
('a123518','asd138','Carlos Soca',123456789012359),
('a123519','asd139','Santiago Ostolaza',123456789012360),
('a123520','asd140','Francisco Arispe',123456789012361),
('a123521','asd141','Rodolfo Moran',123456789012362),
('a123522','asd142','Pedro olivieri',123456789012363),
('a123523','asd143','Domingo Perez',123456789012364),
('a123524','asd144','Rafael Villazan',123456789012365),
('a123525','asd145','Antonio Urdinaran',123456789012366),
('a123526','asd146','Wilmar Cabrera',123456789012367),
('a123527','asd147','Alexander Medina',123456789012368),
('a123528','asd148','Diego Polenta',123456789012369),
('a123529','asd149','Damian Rodriguez',123456789012370),
('a123530','asd150','Santiago Romero',123456789012371),
('a123531','asd151','Abdon Porte',123456789012372),
('a123532','asd152','Martin Del Campo',123456789012373),
('a123533','asd153','Ruben Sosa',123456789012374),
('a123534','asd154','Jose Oyarbide',123456789012375),
('a123535','asd155','Ricardo Perdomo',123456789012376),
('a123536','asd156','Felipe Carballo',123456789012377),
('a123537','asd157','Hugo De Leon',123456789012378),
('a123538','asd158','Alfredo de Santos',123456789012379),
('a123539','asd159','Richard Morales',123456789012380),
('a123540','asd160','Javier Ambrois',123456789012381),
('a123541','asd161','Mauricio Victorino',123456789012382),
('a123542','asd162','Hector Moran',123456789012383),
('a123543','asd163','Eduardo de la Peña',123456789012384),
('a123544','asd164','Jorge Bava',123456789012385),
('a123545','asd165','Alfonso Espino',123456789012386),
('a123546','asd166','Gabriel Alvez',123456789012387),
('a123547','asd167','Gianni Guigou',123456789012388),
('a123548','asd168','Alvaro Recoba',123456789012389),
('a123549','asd169','Jorge Da Costa',123456789012390),
('a123550','asd170','Gonzalo Bergessio',123456789012391),
('a123551','asd171','Adrian Romero',123456789012392),
('a123552','asd172','Esteban Conde',123456789012393),
('a123553','asd173','Carlos Camejo',123456789012394),
('a123554','asd174','Mario Regueiro',123456789012395),
('a123555','asd175','Rafael Garcia',123456789012396)
go

Insert Empleados(usuario,contrasenia,nombre,cargo) values
('Emp1','123asd','Richard Porta','Vendedor'),
('Emp2','qwe123','Luis Mejía','Gerente'),
('Emp3','123asd','Jere Recoba','Admin'),
('Emp4','qwe213','Alvaro Recoba','Vendedor'),
('Emp5','123asd','Juan Izquierdo','Gerente'),
('Emp6','qwe213','Leandro Lozano','Admin'),
('Emp7','123asd','Diego Polenta','Gerente'),
('Emp8','qwe213','','Vendedor'),
('Emp9','123asd','Jere Recoba','Admin'),
('Emp10','qwe213','Richard Porta','Vendedor')
go

insert Ciudades(CodigoC,ciudad,Pais) values
('MVDURU','Montevideo','Uruguay'),
('BASARG','Buenos Aires','Argentina'),
('RJABRS','Rio de Janeiro','Brasil'),
('FLOBRS','Florianopolis','Brasil'),
('LMAPRU','Lima','Peru'),
('ASUPAR','ASunción','Paraguay'),
('MDFMEX','México DF','México'),
('BOGCOL','Bogotá','Colombia'),
('LAPBOL','La Paz','Bolivia'),
('STGCHI','Santiago de Chile','Chile'),
('QTOECU','Quito ','Ecuador'),
('CARVEN','Caracas','Venezuela'),
('MIAUSA','Miami','Estados Unidos')
go

insert Aeropuertos(codigoA,codigoC,direccion,impuestoPartida,impuestoLlegada,nombreA) values
('MVD','MVDURU','Av de las Americas S/N',0.00,100.00,'Carrasco'),
('BAS','BASARG','Acceso a Aeropuerto ',2000.00,1000.00,'Ezeiza'),
('RJA','RJABRS','Praca Salgado filho S/N',80.00,70.00,'Santos Dumont'),
('FLO','FLOBRS','Rua ac ao Aeroporto',90.00,65.00,'Hercilio Luz'),
('LMA','LMAPRU','Av Elmer Faucetti S/N',30.00,20.00,'Jorge Chavez'),
('ASU','ASUPAR','Silvio Petirossi S/N',777.00,666.00,'Silvio Petirossi'),
('MDF','MDFMEX','Av. Capitán Carlos León S/N',12.00,80.50,'Benito Juárez'),
('BOG','BOGCOL','Av. El Dorado #103',50.00,105.00,'El Dorado'),
('LAP','LAPBOL','Huascar 204',60.00,10.00,'El Alto'),
('STG','STGCHI','Av. Armando Cortínez Ote. 1704',35.00,70.00,'Santiago'),
('QTO','QTOECU','Tababela Parish S/N',45.00,200.00,'Mariscal Sucre'),
('MIA','MIAUSA','2100 NW 42nd Ave',150.00,300.00,'Miami'),
('CAR','CARVEN','Maiquetía 1162',600.00,1050.00,'Maiquetía SB')
go

insert Vuelos(codigoV,fechaD,fechaA,Precio,cantAsientos,codigoA,codigoB) values
('202509202050MVD','20250920 20:50','20250921 19:30' ,700.00,250,'MVD','BAS'),
('202509202100BAS','20250920 21:00','20250921 17:50' ,900.00,250,'BAS','RJA'),
('202510211530MVD','20251021 15:30','20251022 16:40' ,1000.00,250,'MVD','ASU'),
('202509211940FLO','20250921 19:40','20250922 07:20' ,600.00,250,'FLO','LMA'),
('202509221650ASU','20250922 16:50','20250923 13:10' ,850.00,250,'ASU','RJA'),
('202509221350RJA','20250922 13:50','20250923 06:50' ,320.00,104,'RJA','FLO'),
('202509230850MVD','20250923 08:50','20250924 14:30' ,280.00,258,'MVD','BAS'),
('202510202050MVD','20251020 20:50','20251021 19:30' ,700.00,250,'MVD','BAS'),
('202509252100BAS','20250925 21:00','20250926 17:50' ,900.00,250,'BAS','RJA'),
('202509211530MVD','20250921 15:30','20250922 16:40' ,1000.00,250,'MVD','ASU'),
('202509202050MDF','20250920 20:50','20250921 19:30',500,100,'MDF','BOG'),
('202509212050STG','20250921 20:50','20250922 19:30',600,150,'STG','LAP'),
('202509222050CAR','20250922 20:50','20250923 19:30',700,200,'CAR','MVD'),
('202509232050QTO','20250923 20:50','20250924 19:30',800,250,'QTO','MIA'),
('202509242050MIA','20250924 20:50','20250925 19:30',900,300,'MIA','BAS'),
('202510250550BAS','20251025 05:50','20251026 19:30',1000,300,'BAS','MIA')
go

insert Venta(fechaCompra,monto,codigoV,nroPasaporte,usuario) values
('20240120 16:40',770.00 ,'202509202050MVD','d702157','Emp1'),
('20240120 18:35',2970.00,'202509202100BAS','a135431','Emp2'),
('20240120 21:05',1600.00 ,'202510211530MVD','c123165','Emp1'),
('20240120 07:50',710.00,'202509211940FLO','b135132','Emp3'),
('20240120 10:30',1717.00,'202509221650ASU','e143546','Emp1'),
('20240120 15:10',465.00,'202509221350RJA','c465464','Emp3'),
('20240120 13:10',1280.00,'202509230850MVD','d702157','Emp2'),
('20240120 16:40',617.00,'202510202050MVD','a123456','Emp5'),
('20240121 16:40',645.00,'202509202050MDF','a123457','Emp6'),
('20240122 16:40',1400.00,'202509212050STG','a123458','Emp7'),
('20240123 16:40',1145.00,'202509222050CAR','a123459','Emp8'),
('20240124 16:40',2050.00,'202509232050QTO','a123460','Emp9'),
('20240125 16:40',1300.00,'202509242050MIA','a123461','Emp10'),
('20240126 16:40',617.00,'202510250550BAS','a123462','Emp5'),
('20240127 16:40',645.00,'202509242050MIA','a123463','Emp6'),
('20240128 16:40',1400.00,'202509232050QTO','a123464','Emp7'),
('20240129 16:40',1145.00,'202509222050CAR','a123465','Emp8'),
('20240130 16:40',2050.00,'202509212050STG','a123466','Emp9'),
('20240131 16:40',1300.00,'202509202050MDF','a123467','Emp10'),
('20240202 16:40',617.00,'202509202050MDF','a123468','Emp1'),
('20240120 18:36',456.00,'202509221350RJA','a123457','Emp1'),
('20240121 18:35',456.00,'202509221350RJA','a123458','Emp1'),
('20240121 18:36',456.00,'202509221350RJA','a123459','Emp1'),
('20240122 18:35',456.00,'202509221350RJA','a123460','Emp1'),
('20240122 18:36',456.00,'202509221350RJA','a123461','Emp1'),
('20240123 18:35',456.00,'202509221350RJA','a123462','Emp1'),
('20240123 18:36',456.00,'202509221350RJA','a123463','Emp1'),
('20240124 18:35',456.00,'202509221350RJA','a123464','Emp1'),
('20240124 18:36',456.00,'202509221350RJA','a123465','Emp1'),
('20240125 18:35',456.00,'202509221350RJA','a123466','Emp1'),
('20240125 18:36',456.00,'202509221350RJA','a123467','Emp1'),
('20240126 18:35',456.00,'202509221350RJA','a123468','Emp1'),
('20240126 18:36',456.00,'202509221350RJA','a123469','Emp1'),
('20240127 18:35',456.00,'202509221350RJA','a123470','Emp1'),
('20240127 18:36',456.00,'202509221350RJA','a123471','Emp1'),
('20240128 18:35',456.00,'202509221350RJA','a123472','Emp1'),
('20240128 18:36',456.00,'202509221350RJA','a123473','Emp1'),
('20240129 18:35',456.00,'202509221350RJA','a123474','Emp1'),
('20240129 18:36',456.00,'202509221350RJA','a123475','Emp1'),
('20240130 18:35',456.00,'202509221350RJA','a123476','Emp1'),
('20240130 18:36',456.00,'202509221350RJA','a123477','Emp1'),
('20240131 18:35',456.00,'202509221350RJA','a123478','Emp1'),
('20240201 18:36',456.00,'202509221350RJA','a123479','Emp1'),
('20240102 18:35',456.00,'202509221350RJA','a123480','Emp1'),
('20240102 18:36',456.00,'202509221350RJA','a123481','Emp1'),
('20240103 18:35',456.00,'202509221350RJA','a123482','Emp1'),
('20240103 18:36',456.00,'202509221350RJA','a123483','Emp1'),
('20240104 18:35',456.00,'202509221350RJA','a123484','Emp1'),
('20240104 18:36',456.00,'202509221350RJA','a123485','Emp1'),
('20240105 18:35',456.00,'202509221350RJA','a123486','Emp1'),
('20240105 18:36',456.00,'202509221350RJA','a123487','Emp1'),
('20240106 18:35',456.00,'202509221350RJA','a123488','Emp1'),
('20240106 18:36',456.00,'202509221350RJA','a123489','Emp1'),
('20240107 18:35',456.00,'202509221350RJA','a123490','Emp1'),
('20240107 18:36',456.00,'202509221350RJA','a123491','Emp1'),
('20240108 18:35',456.00,'202509221350RJA','a123492','Emp1'),
('20240108 18:36',456.00,'202509221350RJA','a123493','Emp1'),
('20240109 18:35',456.00,'202509221350RJA','a123494','Emp1'),
('20240109 18:36',456.00,'202509221350RJA','a123495','Emp1'),
('20240110 18:35',456.00,'202509221350RJA','a123496','Emp1'),
('20240110 18:36',456.00,'202509221350RJA','a123497','Emp1'),
('20240111 18:35',456.00,'202509221350RJA','a123498','Emp1'),
('20240111 18:36',456.00,'202509221350RJA','a123499','Emp1'),
('20240112 18:35',456.00,'202509221350RJA','a123500','Emp1'),
('20240112 18:36',456.00,'202509221350RJA','a123501','Emp1'),
('20240113 18:35',456.00,'202509221350RJA','a123502','Emp1'),
('20240113 18:36',456.00,'202509221350RJA','a123503','Emp1'),
('20240114 18:35',456.00,'202509221350RJA','a123504','Emp1'),
('20240114 18:36',456.00,'202509221350RJA','a123505','Emp1'),
('20240115 18:35',456.00,'202509221350RJA','a123506','Emp1'),
('20240115 18:36',456.00,'202509221350RJA','a123507','Emp1'),
('20240116 18:35',456.00,'202509221350RJA','a123508','Emp1'),
('20240116 18:36',456.00,'202509221350RJA','a123509','Emp1'),
('20240117 18:35',456.00,'202509221350RJA','a123510','Emp1'),
('20240117 18:36',456.00,'202509221350RJA','a123511','Emp1'),
('20240118 18:35',456.00,'202509221350RJA','a123512','Emp1'),
('20240118 18:36',456.00,'202509221350RJA','a123513','Emp1'),
('20240119 18:35',456.00,'202509221350RJA','a123514','Emp1'),
('20240119 18:36',456.00,'202509221350RJA','a123515','Emp1'),
('20240120 18:35',456.00,'202509221350RJA','a123516','Emp1'),
('20240120 18:36',456.00,'202509221350RJA','a123517','Emp1'),
('20240121 18:35',456.00,'202509221350RJA','a123518','Emp1'),
('20240121 18:36',456.00,'202509221350RJA','a123519','Emp1'),
('20240122 18:35',456.00,'202509221350RJA','a123520','Emp1'),
('20240122 18:36',456.00,'202509221350RJA','a123521','Emp1'),
('20240123 18:35',456.00,'202509221350RJA','a123522','Emp1'),
('20240123 18:36',456.00,'202509221350RJA','a123523','Emp1'),
('20240124 18:35',456.00,'202509221350RJA','a123524','Emp1'),
('20240124 18:36',456.00,'202509221350RJA','a123525','Emp1'),
('20240125 18:35',456.00,'202509221350RJA','a123526','Emp1'),
('20240125 18:36',456.00,'202509221350RJA','a123527','Emp1'),
('20240126 18:35',456.00,'202509221350RJA','a123528','Emp1'),
('20240126 18:36',456.00,'202509221350RJA','a123529','Emp1'),
('20240127 18:35',456.00,'202509221350RJA','a123530','Emp1'),
('20240127 18:36',456.00,'202509221350RJA','a123531','Emp1'),
('20240128 18:35',456.00,'202509221350RJA','a123532','Emp1'),
('20240128 18:36',456.00,'202509221350RJA','a123533','Emp1'),
('20240129 18:35',456.00,'202509221350RJA','a123534','Emp1'),
('20240129 18:36',456.00,'202509221350RJA','a123535','Emp1'),
('20240130 18:35',456.00,'202509221350RJA','a123536','Emp1'),
('20240130 18:36',456.00,'202509221350RJA','a123537','Emp1'),
('20240201 18:35',456.00,'202509221350RJA','a123538','Emp1'),
('20240201 18:36',456.00,'202509221350RJA','a123539','Emp1'),
('20240202 18:35',456.00,'202509221350RJA','a123540','Emp1'),
('20240202 18:36',456.00,'202509221350RJA','a123541','Emp1'),
('20240203 18:35',456.00,'202509221350RJA','a123542','Emp1'),
('20240203 18:36',456.00,'202509221350RJA','a123543','Emp1'),
('20240204 18:35',456.00,'202509221350RJA','a123544','Emp1'),
('20240204 18:36',456.00,'202509221350RJA','a123545','Emp1'),
('20240205 18:35',456.00,'202509221350RJA','a123546','Emp1'),
('20240205 18:36',456.00,'202509221350RJA','a123547','Emp1'),
('20240206 18:35',456.00,'202509221350RJA','a123548','Emp1'),
('20240206 18:36',456.00,'202509221350RJA','a123549','Emp1'),
('20240207 18:35',456.00,'202509221350RJA','a123550','Emp1'),
('20240207 18:36',456.00,'202509221350RJA','a123551','Emp1'),
('20240208 18:35',456.00,'202509221350RJA','a123552','Emp1'),
('20240208 18:36',456.00,'202509221350RJA','a123553','Emp1'),
('20240209 18:35',456.00,'202509221350RJA','a123554','Emp1'),
('20240209 18:36',456.00,'202509221350RJA','a123555','Emp1'),
('20240208 18:35',456.00,'202509221350RJA','d702157','Emp1'),
('20240208 18:36',456.00,'202509221350RJA','a135431','Emp1'),
('20240209 18:35',456.00,'202509221350RJA','b135132','Emp1'),
('20240209 18:36',456.00,'202509221350RJA','c123165','Emp1')
go

insert Adquieren(nroPasaporte,nroTicket,nroAsiento) values
('d702157',123,122),
('a135431',2,155),
('b135132',3,155),
('c123165',4,155),
('b135132',5,155),
('e143546',6,100),
('b135132',7,155),
('a123456',8,75),
('a123457',9,2),
('a123458',10,12),
('a123459',11,36),
('a123460',12,45),
('a123461',13,100),
('a123456',21,1),
('a123457',22,2),
('a123458',23,3),
('a123459',24,4),
('a123460',25,5),
('a123461',26,6),
('a123462',27,7),
('a123463',28,8),
('a123464',29,9),
('a123465',30,10),
('a123466',31,11),
('a123467',32,12),
('a123468',33,13),
('a123469',34,14),
('a123470',35,15),
('a123471',36,16),
('a123472',37,17),
('a123473',38,18),
('a123474',39,19),
('a123475',40,20),
('a123476',41,21),
('a123477',42,22),
('a123478',43,23),
('a123479',44,24),
('a123480',45,25),
('a123481',46,26),
('a123482',47,27),
('a123483',48,28),
('a123484',49,29),
('a123485',50,30),
('a123486',51,31),
('a123487',52,32),
('a123488',53,33),
('a123489',54,34),
('a123490',55,35),
('a123491',56,36),
('a123492',57,37),
('a123493',58,38),
('a123494',59,39),
('a123495',60,40),
('a123496',61,41),
('a123497',62,42),
('a123498',63,43),
('a123499',64,44),
('a123500',65,45),
('a123501',66,46),
('a123502',67,47),
('a123503',68,48),
('a123504',69,49),
('a123505',70,50),
('a123506',71,51),
('a123507',72,52),
('a123508',73,53),
('a123509',74,54),
('a123510',75,55),
('a123511',76,56),
('a123512',77,57),
('a123513',78,58),
('a123514',79,59),
('a123515',80,60),
('a123516',81,61),
('a123517',82,62),
('a123518',83,63),
('a123519',84,64),
('a123520',85,65),
('a123521',86,66),
('a123522',87,67),
('a123523',88,68),
('a123524',89,69),
('a123525',90,70),
('a123526',91,71),
('a123527',92,72),
('a123528',93,73),
('a123529',94,74),
('a123530',95,75),
('a123531',96,76),
('a123532',97,77),
('a123533',98,78),
('a123534',99,79),
('a123535',100,80),
('a123536',101,81),
('a123537',102,82),
('a123538',103,83),
('a123539',104,84),
('a123540',105,85),
('a123541',106,86),
('a123542',107,87),
('a123543',108,88),
('a123544',109,89),
('a123545',110,90),
('a123546',111,91),
('a123547',112,92),
('a123548',113,93),
('a123549',114,94),
('a123550',115,95),
('a123551',116,96),
('a123552',117,97),
('a123553',118,98),
('a123554',119,99),
('d702157',120,101),
('a135431',121,102),
('b135132',122,103)
go
---SP-----

--Ciudades--
create proc BuscarCiudades
@cod varchar(6)
as
begin
	select *
	from Ciudades
	where CodigoC = @cod
end
go

create proc BuscarCiudadesActivas
@cod varchar(6)
as
begin
	select *
	from Ciudades
	where CodigoC = @cod and activo = 1
end
go

create proc AltaCiudades
@cod varchar(6),
@pais varchar(20),
@nom varchar(20)
as
begin
	if exists(select * from Ciudades where CodigoC = @cod and activo = 1)
		return -1
		
	if exists(select * from Ciudades where CodigoC = @cod and activo = 0)
	begin
		update Ciudades 
		set pais= @pais,ciudad = @nom , activo =1
		where codigoC = @cod 
		
		return 1
	end
	
	insert into Ciudades(codigoC,ciudad,pais) values(@cod,@nom,@pais)

end 
go

create proc ModificarCiudades
@cod varchar(6),
@pais varchar(20),
@nom varchar(20)
as
begin 
	if not exists(select * from Ciudades where CodigoC = @cod and activo = 1)
		Begin 
			return -1
		end
	else
		begin	
			update Ciudades
			set Pais = @pais,
			ciudad = @nom 
			where CodigoC = @cod
				if @@ERROR = 0
					return 1
				Else
					return -2
		end
end
go

create proc BajaCiudades
@cod varchar(6)
as
begin 
	if not exists(select * from Ciudades where CodigoC = @cod)
		return -1
	
	if exists (select * from Aeropuertos where CodigoC = @cod)
		begin
			update Ciudades
			set activo = 0
			where CodigoC = @cod
		end
	else
		begin 
			delete from Ciudades where CodigoC = @cod
			if @@ERROR <> 0
				return -2
		end	
	return	1
end
go

create proc ListarCiudades
as
begin
	Select *
	from Ciudades
	where activo = 1
end
go

--Cliente
create proc BuscarC
@pasaporte varchar(7)
as
begin 
	select *
	from Clientes
	where NroPasaporte = @pasaporte
end	
go

create proc BuscarClienteActivo
@pasaporte varchar(7)
as
begin 
	select *
	from Clientes
	where NroPasaporte = @pasaporte
	and activo = 1
end	
go

create proc AltaClientes
@pasaporte varchar (7),
@pass varchar(15),
@nom varchar(30),
@tarj bigint
as
begin
	if exists(select * from Clientes where NroPasaporte = @pasaporte and activo = 1)
		return -1
		
	if exists(select * from Clientes where NroPasaporte = @pasaporte and activo = 0)
	begin 
		update Clientes
		set contrasenia = @pass,nombre = @nom,nroTarjeta= @tarj , activo = 1
		where nroPasaporte = @nom

		return	1
	end
	
	insert into Clientes(nroPasaporte,contrasenia,nombre,nrotarjeta) values (@pasaporte,@pass,@nom ,@tarj)
		
	
end
go

create proc BajaClientes
@pasaporte varchar(7)
as
begin 
	if not exists(select * from Clientes where NroPasaporte = @pasaporte)
		return -1
		
	if exists (select * from Venta where nroPasaporte = @pasaporte)
		begin 
			update Clientes
			set activo = 0
			where nroPasaporte = @pasaporte
		end
	else if exists (select * from Adquieren where nroPasaporte = @pasaporte)
		begin 
			update Clientes
			set activo = 0
			where nroPasaporte = @pasaporte
		end
	else 
		begin 
			delete from Clientes where NroPasaporte = @pasaporte
				if @@ERROR <> 0
					return 1
		end
end
go

create proc ModificarClientes 
@pasaporte varchar (7),
@pass varchar(15),
@nom varchar(30),
@tarj bigint
as
begin
	if not exists(select * from Clientes where NroPasaporte = @pasaporte and activo = 1)
		begin
			return -1
		end
	else
		begin
			update Clientes
			set contrasenia = @pass,
				Nombre = @nom, 
				nrotarjeta = @tarj
			where NroPasaporte = @pasaporte
				if @@ERROR = 0
					return 1
				Else
					return -2
		end
end
go

create proc ListarClientes
as
begin 
	Select *
	from Clientes
	where activo = 1
end
go

--Alta Venta
create proc AltaVenta
@precio float,
@codigo varchar(15),
@pasaporte varchar(7),
@usuario varchar(20)
as
begin
	if not exists (select * from Vuelos where CodigoV = @codigo)
		return -1
		
	if not exists (select * from Clientes where NroPasaporte = @pasaporte)
		return -2
		
	insert into Venta (monto,codigoV,nroPasaporte,usuario) 
	values (@precio,@codigo,@pasaporte,@usuario)
		if @@ERROR <>0
			return -4
		else
			return @@identity
end
go

create proc AltaAdquieren
@asiento int,
@ticket int,
@pasaporte varchar(7)
as
begin 
	if not exists (select * from Clientes where nroPasaporte = @pasaporte and activo = 1)
		return -1
	
	if not exists (select * from Venta where nroTicket= @ticket)
		return -2
	
	declare @cod varchar(15)
	declare @vendidos int
	declare @cant int 
	
	select @cod = v.codigoV, @cant = f.cantAsientos from Venta v inner join Vuelos f on v.codigoV = f.codigoV where v.nroTicket = @ticket
			
	if exists(select * from Adquieren a inner join Venta v on a.nroTicket = v.nroTicket where v.codigoV = @cod and a.nroAsiento = @asiento)
		return -3

	select @vendidos = COUNT(v.codigoV)
	from Venta v inner join Adquieren a on v.nroTicket = a.nroTicket
	where v.codigoV = @cod
	
	if (@vendidos > @cant)
		return -4
			
	insert into Adquieren(nroPasaporte,nroTicket,nroAsiento)
	values (@pasaporte,@ticket,@asiento)
		return 1

end
go

create proc MostrarVenta
@factura int
as
begin
	select * 
	from Venta 
	where nroTicket = @factura
end
go

create proc ListadoVenta
as
begin
	select * 
	from Venta 
end
go

create proc ListadoAdquieren
as
begin 
	select *
	from Adquieren
end
go

--Aeropuerto--
create proc BuscarAeropuerto
@cod varchar(3)
as
begin 
	select *
	from Aeropuertos
	where CodigoA = @cod
end
go

create proc BuscarAeropuertoActivo
@cod varchar(3)
as
begin 
	select *
	from Aeropuertos
	where CodigoA = @cod and activo = 1
end
go

create proc AltaAeropuertos
@cod varchar(3),
@nom varchar(20),
@dir varchar (30),
@partida float,
@llegada float,
@ciu varchar(6)
as
begin
	if exists(select * from Aeropuertos where CodigoA = @cod and activo = 1)
		return -1
	
	if not exists(select * from Ciudades where CodigoC = @ciu and activo = 1)
		return -2
		
	if exists(select * from Aeropuertos where codigoA = @cod)
		begin	
			update Aeropuertos
			set nombreA = @nom,direccion = @dir,impuestoPartida = @partida,impuestoLlegada = @llegada,codigoC = @ciu, activo = 1
			where codigoA = @cod
			if(@@ERROR<>0)
				return -3
				
			return 1
		end
	else
		begin
			insert into Aeropuertos(codigoA,nombreA,direccion,impuestoPartida,impuestoLlegada,codigoC) 
			values(@cod,@nom,@dir,@partida,@llegada,@ciu)
				return 1
		end
end
go

create proc BajaAeropuertos
@cod varchar(3)
as
begin 
	if not exists(select * from Aeropuertos where CodigoA = @cod)
		return -1
		
	if exists (select * from vuelos where codigoA = @cod or codigoB = @cod)
		begin
			update Aeropuertos
			set activo = 0
			where codigoA = @cod
		end
				
	delete from Aeropuertos where codigoA = @cod
		return 1			
end
go

Create proc ModificarAeropuertos
@cod varchar(3),
@nom varchar(20),
@dir varchar (30),
@partida float,
@llegada float,
@codC varchar(6)
as
begin 
	if not exists (select * from Ciudades where codigoC = @codC and activo = 1)
		return -1
		
	if not exists(select * from Aeropuertos where codigoA = @cod and activo = 1)
		return -2

	update Aeropuertos
	set Direccion = @dir,
		NombreA = @nom,
		impuestoPartida= @partida,
		impuestoLlegada =@llegada, 
		codigoc = @codC
	where CodigoA = @cod
	if(@@ERROR<> 0)
		return -3
			
	return 1
end
go

create proc ListadoAeropuertos
as
begin 
	select *
	from Aeropuertos
	where activo = 1
end
go

--Vuelos
create proc AltaVuelos
@fechaS datetime,
@fechaL datetime,
@precio money,
@asientos int,
@codA varchar(3),
@codB varchar(3),
@codigoV varchar(15) output
as
begin 
	if not exists(select * from Aeropuertos where CodigoA = @codA and activo =1)
		return -1

	if not exists(select * from Aeropuertos where CodigoA = @codB and activo =1)
		return -2

	set @codigoV = CONVERT(varchar(8),@fechas,112)+ CONVERT(varchar(2),DATEPART(hour,@fechas)) + CONVERT(varchar(2),DATEPART(MINUTE,@fechas))+ @codA
	 
	Insert into Vuelos (fechaD,fechaA,Precio,cantAsientos,CodigoA,CodigoB,codigov) 
	values(@fechaS,@fechaL,@precio,@asientos,@codA,@codB,@codigoV)
		if @@ERROR <>0
			return -3
		else
			return  1
end 
go

create proc BuscarVuelos
@codV vaRCHAR(15)
as
begin
	select *
	from Vuelos
	Where codigoV = @codV
end
go

create proc ListadoVuelos
as
begin
	select *
	from Vuelos
end
go

--Empleados--
create proc BuscarE
@usu varchar(20)
as
begin 
	select *
	from Empleados
	Where usuario = @usu
end
go

create proc LogueoE
@usu varchar(20),
@pass varchar(15)
as
begin
	select *
	from Empleados
	where Usuario = @usu and
	Contrasenia = @pass 
end
go

create proc UsuarioSQL 
@usu varchar(10),
@pass varchar(15)
as
begin 
	Declare @variable varchar(200)
	
	if not exists(select * from Empleados where usuario = @usu)
		return -1
			
	if exists(select * from Empleados where usuario = @usu and contrasenia = @pass)
	begin
		begin try
			
			set @variable = 'Create Login [' + @usu + '] With Password = ' + QUOTENAME(@pass,'''')
			exec (@variable)
			
			set @variable = 'Create user [' + @usu + '] From Login [' + @usu + ']'
			exec (@variable)
			
			set @variable = 'Grant Execute To [' + @usu +']'
			exec (@variable)
			
			return 1
		end try
		begin catch
				return -2
		end catch
	end
	 
	return 1
end
go

--declare @ret int
--exec @ret = du 'Emp2','qwe123'

--if (@ret = -1)
--	print'Error en la empleado.'
--else if (@ret = -2)
--    print'Error en el login.'
--else if (@ret = -3)
--    print'Error en el user.' 
--else if (@ret = -4)
--    print'Error en el grant.'   
--else if (@ret = 1)
--	print 'correcto'