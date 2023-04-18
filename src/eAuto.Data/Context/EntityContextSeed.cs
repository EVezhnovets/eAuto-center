﻿using eAuto.Data.Converters;
using eAuto.Data.Interfaces.DataModels;
using eAuto.Data.Interfaces.Enum;
using Microsoft.EntityFrameworkCore;

namespace eAuto.Data.Context
{
	public class EntityContextSeed
	{
		public static async Task SeedAsync(
			EAutoContext context, 
			int retry = 0)
		{
			var retryForAvailability = retry;

			try
			{
				if(!await context.BodyTypes.AnyAsync())
				{
					await context.AddRangeAsync(GetPreConfiguredBodyTypes());

					await context.SaveChangesAsync();
				}

				if (!await context.Brands.AnyAsync())
				{
					await context.AddRangeAsync(GetPreConfiguredBrands());

					await context.SaveChangesAsync();
				}

				if (!await context.DriveTypes.AnyAsync())
				{
					await context.AddRangeAsync(GetPreConfiguredDriveTypes());

					await context.SaveChangesAsync();
				}

				if (!await context.Transmissions.AnyAsync())
				{
					await context.AddRangeAsync(GetPreConfiguredTransmissions());

					await context.SaveChangesAsync();
				}

				if (!await context.Models.AnyAsync())
				{
					await context.AddRangeAsync(GetPreConfiguredModels());

					await context.SaveChangesAsync();
				}

				if (!await context.Generations.AnyAsync())
				{
					await context.AddRangeAsync(GetPreConfiguredGenerations());

					await context.SaveChangesAsync();
				}

				if (!await context.EngineTypes.AnyAsync())
				{
					await context.AddRangeAsync(GetPreConfiguredEngineTypes());

					await context.SaveChangesAsync();
				}

				if (!await context.Cars.AnyAsync())
				{
					await context.AddRangeAsync(GetPreConfiguredCars());

					await context.SaveChangesAsync();
				}
			}
			catch (Exception ex)
			{
				if (retryForAvailability >= 10) throw;

				retryForAvailability++;

				await SeedAsync(context, retry);
			}
		}

		private static IEnumerable<BodyTypeDataModel> GetPreConfiguredBodyTypes()
		{
			return new List<BodyTypeDataModel>
			{
				new("Sedan"),
				new("Liftback"),
				new("Сrossover"),
				new("Minibus"),
				new("Minivan"),
				new("Van"),
				new("SUV"),
				new("Station Wagon"),
			};
		}
		private static IEnumerable<BrandDataModel> GetPreConfiguredBrands()
		{
			return new List<BrandDataModel>
			{
				new("Alfa Romeo"),
				new("Audi"),
				new("BMW"),
				new("Chevrolet"),
				new("Chrysler"),
				new("Citroen"),
				new("Dodge"),
				new("Fiat"),
				new("Ford"),
				new("Geely"),
				new("Honda"),
				new("Hyndai"),
				new("Kia"),
				new("LADA"),
				new("Lexus"),
				new("Mazda"),
				new("Mercedes-Benz"),
				new("Mitsubishi"),
				new("Nissan"),
				new("Opel"),
				new("Peugeot"),
				new("Renault"),
				new("Rover"),
				new("SEAT"),
				new("Skoda"),
				new("Subaru"),
				new("Suzuki"),
				new("Toyota"),
				new("Volkswagen"),
				new("Volvo"),
				new("Jaguar"),
				new("Land Rover"),
				new("Jeep"),
				new("Genesis"),
				new("Acura"),
				new("Haval"),
				new("Mini"),
			};
		}

		private static IEnumerable<DriveTypeDataModel> GetPreConfiguredDriveTypes()
		{
			return new List<DriveTypeDataModel>()
			{
				new("Rear Wheel Drive"),
				new("Front Wheel Drive"),
				new("All Wheel Drive")
			};
		}
		private static IEnumerable<TransmissionDataModel> GetPreConfiguredTransmissions()
		{
			return new List<TransmissionDataModel>()
			{
				new ("Manual"),
				new ("Automatic")
			};
		}

		private static IEnumerable<ModelDataModel> GetPreConfiguredModels()
		{
			return new List<ModelDataModel>()
			{
				new("XC 90", 30),
				new("XC 60", 30),
				new("V 40", 30),
				new("V 60", 30),
				new("V 90", 30),
				new("S 60", 30),
				new("Q7", 2),
				new("Q5", 2),
				new("Range Rover Sport", 32),
				new("Range Rover Velar", 32),
				new("Range Rover Discovery", 32),
				new("Arteon", 29),
				new("Tiguan", 29),
				new("Touareg", 29),
				new("XE", 31),
				new("F-Pace", 31),
				new("I-Pace", 31),
				new("F-Type", 31),
			};
		}

		private static IEnumerable<GenerationDataModel> GetPreConfiguredGenerations()
		{
			return new List<GenerationDataModel>()
			{
				new("4M, 2015-2019", 2, 7),
				new("FY, 2016-2020", 2, 8),
				new("I, 2017-2023", 32, 10),
				new("III, 2022...", 32, 9),
				new("II Restyling, 2020...", 29, 13),
				new("III, 2018...", 29, 14),
				new("II Restyling, 2019...", 28, 1),
				new("II, 2016-2020", 30, 5),
				new("I, 2016-2020", 31, 16),
			};
		}

		private static IEnumerable<EngineTypeDataModel> GetPreConfiguredEngineTypes()
		{
			return new List<EngineTypeDataModel>()
			{
				new(EngineTypesEnumConverter.ConvertToDbValue(EngineTypesEnum.Gasoline)),
				new(EngineTypesEnumConverter.ConvertToDbValue(EngineTypesEnum.Diesel)),
				new(EngineTypesEnumConverter.ConvertToDbValue(EngineTypesEnum.Electric)),
				new(EngineTypesEnumConverter.ConvertToDbValue(EngineTypesEnum.Gas)),
				new(EngineTypesEnumConverter.ConvertToDbValue(EngineTypesEnum.GasolineGibrid)),
				new(EngineTypesEnumConverter.ConvertToDbValue(EngineTypesEnum.DieselGibrid))
			};
		}
		private static IEnumerable<CarDataModel> GetPreConfiguredCars()
		{
			return new List<CarDataModel>()
			{
				new(60000,
				"\\images\\cars\\063dc23b-c2e3-4af0-8a8a-85ba67af26d7.jpg",
				Convert.ToDateTime("01.01.2018"),
				DateTime.Now,
				160000,
				"<p>3.0T Quattro,&nbsp;7 places,&nbsp;Top-of-the-range Prestige Premium+, factory antichrome, 21 l / d on almost new all-season tires, Door closers, Panorama with sunroof, Heated and ventilated seats, Heated steering wheel, Multi-zone climate control, Cameras 360, Bose premium audio system, Dead zone sensors</p>",
				null,
				EngineTypesEnumConverter.ConvertToDbValue(EngineTypesEnum.Diesel),
				2,
				"3.0",
				null,
				2,
				7,
				1,
				7,
				3,
				2),

                new(34700,
                "\\images\\cars\\80a3d6f3-ffee-4e61-933f-7d40056a2508.jpg",
                Convert.ToDateTime("01.01.2019"),
                DateTime.Now,
                138000,
                "<p>Audi Q5 2.0 TDI quattro S tronic.&nbsp;The car is in excellent condition. Fresh MOT according to the regulations from an authorized dealer in Germany, without a run in the Republic of Belarus. Without painted elements and mileage adjustments.&nbsp;Lane keeping assistant.&nbsp;Adaptive cruise control (ACCS).&nbsp;Rain sensor.&nbsp;Light sensor.&nbsp;Power trunk. Electric turnbuckle. Power front seats.&nbsp;Heated front seats</p>",
				null,
                EngineTypesEnumConverter.ConvertToDbValue(EngineTypesEnum.Diesel),
				2,
                "2.0",
                null,
                2,
				8,
				2,
				7,
				3,
				2),

                new(225000,
                "\\images\\cars\\1bbfc189-58f2-4f01-ab08-429fef0a5eba.jpg",
                Convert.ToDateTime("01.01.2022"),
                DateTime.Now,
                29,
                "<p>New car. With VAT 20%.</p>\r\n<p>Equipment AUTOBIOGRAPHY</p>\r\n<p>&bull; Heated windshield</p>\r\n<p>&bull; Heated washer jets</p>\r\n<p>&bull; Heated steering wheel</p>\r\n<p>&bull; Autobiography badge</p>\r\n<p>&bull; Electrically adjustable exterior mirrors, heated, memory function, ambient lights and auto-dimming function (driver's side)</p>\r\n<p>&bull; Heated rear window</p>\r\n<p>&bull; Rear window wiper</p>\r\n<p>&bull; Windshield wipers with rain sensors</p>\r\n<p>&bull; Winter parking position of cleaners</p>\r\n<p>&bull; Automatic headlights</p>\r\n<p>&bull; High pressure headlight washers</p>\r\n<p>&bull; Automatic headlight range adjustment</p>\r\n<p>&bull; Rear fog lights</p>",
                null,
				EngineTypesEnumConverter.ConvertToDbValue(EngineTypesEnum.Gasoline),
				1,
                "3.0",
                null,
                32,
                9,
                4,
                7,
                3,
                2),

                new(42980,
                "\\images\\cars\\74d95594-aeae-42a1-b565-7ead89e871d0.jpg",
                Convert.ToDateTime("01.01.2020"),
                DateTime.Now,
                40000,
                "<p>With VAT 20%. One owner.</p>\r\n<p>Full service history at office. Volkswagen dealer. Warranty until 02/03/2024.</p>\r\n<p>The car is located on the trading platform Vernye avto at st. Pritytsky 60th century.</p>\r\n<p>We provide a guarantee of legal purity.</p>\r\n<p>Options: keyless entry, digital dashboard, LED light, atmospheric interior lighting, 3-zone climate control, cruise control, distance control, lane control, el. side mirror adjustment, heated mirrors, heated windshield, heated front and rear seats, rear view camera, parking sensors, anti-lock braking system (ABS), brake force distribution (EBD), brake assist system (BAS), electronic stability control (ESP) ).</p>",
                "",
				EngineTypesEnumConverter.ConvertToDbValue(EngineTypesEnum.Gasoline),
				1,
                "2.0",
                null,
                29,
                13,
                5,
                7,
                3,
                2),

                new(59000,
                "\\images\\cars\\67b33629-db15-411c-8b40-d10fde20991a.jpg",
                Convert.ToDateTime("01.01.2018"),
                DateTime.Now,
                59000,
                "<p>Car from Germany. Completely cleared. Without run across RB and RF.</p>\r\n<p>Ideal condition. Original mileage, all elements without colors.</p>\r\n<p>Air suspension, electric hitch, DYNAUDIO music, TRAILER ASSIST, lane keeping.</p>\r\n<p>MOT passed.</p>\r\n<p>Ready to visit the official dealer at your expense.</p>",
                "",
				EngineTypesEnumConverter.ConvertToDbValue(EngineTypesEnum.Diesel),
				2,
                "3.0",
                null,
                29,
                14,
                6,
                7,
                3,
                2),

                new(57200,
                "\\images\\cars\\459ff4b0-e7e6-4023-bbfd-cc48812faaf5.jpg",
                Convert.ToDateTime("01.01.2019"),
                DateTime.Now,
                68000,
                "<p>Volvo XC90 Inscription flagship crossover for sale!</p>\r\n<p>The car was bought in Germany from an authorized dealer Hannover VolvoGmbh (and serviced there).</p>\r\n<p>Restyled 2020 car with the patented Mild HybridVolvo B5, combined with the notorious</p>\r\n<p>\"Power Pulse\" - additional engine agility (only in D5 engines).</p>\r\n<p>Interior: nice perforated Nappa leather, natural wood inserts and black ceiling, seats with ventilation and massage.</p>\r\n<p>The car is in excellent condition both technically and aesthetically. The entire package of documents and service history.</p>",
                "",
				EngineTypesEnumConverter.ConvertToDbValue(EngineTypesEnum.DieselGibrid),
				6,
                "2.0",
                null,
                30,
                1,
                7,
                7,
                3,
                2),

                new(43500,
                "\\images\\cars\\ef6b5b97-051f-4cec-b98f-c425774cca85.jpg",
                Convert.ToDateTime("01.01.2019"),
                DateTime.Now,
                103000,
                "<p>Model year 2020, B4204T34(T8) plug in hybrid engine (can run on both electric and gasoline engines), power 390 hp (303 hp ICE + 87 hp electric motor), acceleration to hundreds of 5.3s, 8-speed automatic transmission Geartronisin Japan, four-wheel drive, double glazing, heated seats (all), ventilation, massage, memory driver, passenger, dashboard in leather, black ceiling, autonomous interior heating, heated steering wheel, autostart and control of all parameters via the Volvo cars app, adaptive cruise, active Pilot, all safety assistants, lane keeping, harman kardon music, electric trunk lid with foot control, headlights FULL LED ACTIVE HIGH BEAM with auto-range and cornering lights, with oncoming traffic cut and much more.</p>",
                "",
				EngineTypesEnumConverter.ConvertToDbValue(EngineTypesEnum.GasolineGibrid),
				5,
                "2.0",
                null,
                30,
                5,
                8,
                7,
                3,
                2),

                new(38350,
                "\\images\\cars\\f78b7cbf-948d-4bfd-a037-23a6e2e8449b.jpg",
                Convert.ToDateTime("01.01.2016"),
                DateTime.Now,
                93600,
                "<p>&bull; Car purchased from an Official Dealer</p>\r\n<p>&bull; Complete set of keys</p>\r\n<p>&bull; Service history</p>\r\n<p>Main characteristics:</p>\r\n<p>&bull; Engine: diesel (300 hp)</p>\r\n<p>&bull; Volume 3000 cm3</p>\r\n<p>Transmission: Automatic</p>\r\n<p>Production date: 2016</p>\r\n<p>&bull; Drive: full</p>\r\n<p>&bull; Vin: SADCA2BK5HA092284</p>",
                "",
				EngineTypesEnumConverter.ConvertToDbValue(EngineTypesEnum.Diesel),
				2,
                "3.0",
                null,
                31,
                16,
                9,
                7,
                3,
                2),

                new(54900,
                "\\images\\cars\\3b4f09e7-2b9d-4729-8914-08cf8410b1d2.jpg",
                Convert.ToDateTime("01.01.2019"),
                DateTime.Now,
                80536,
                "<p>The car was purchased and serviced from the official Land Rover dealer in Minsk.</p>\r\n<p>S package</p>\r\n<p>2.0 diesel, 180 hp</p>\r\n<p>- Premium Exterior Package (Includes: Front Fog Lights, Lower Blade in Atlas, Lowers in Narvik Black)</p>\r\n<p>- Perforated grained leather and suede seat trim</p>\r\n<p>- Rear view camera (including washer)</p>\r\n<p>- Cruise control with speed limiter</p>\r\n<p>- Dual zone climate control</p>\r\n<p>- Meridian 380W audio system with 11 speakers</p>\r\n<p>- 10-way seat adjustment, driver's seat memory and heated front seats</p>\r\n<p>- Electric tailgate with touch-sensitive opening (foot movement)</p>\r\n<p>- Heated windshield</p>\r\n<p>&nbsp;</p>",
                "",
                EngineTypesEnumConverter.ConvertToDbValue(EngineTypesEnum.Diesel),
				2,
                "2.0",
                null,
                32,
                10,
                3,
                7,
                3,
                2),
            };
		}
	}
}