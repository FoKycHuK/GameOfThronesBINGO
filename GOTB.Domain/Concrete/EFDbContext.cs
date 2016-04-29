using System.Data.Entity;
using GoTB.Domain.Entities;

namespace GoTB.Domain.Concrete
{
    public class EFDbContext : DbContext
    {
        public DbSet<Character> Characters { get; set; }

        public static void CreateDefaultsValues(EFDbContext context)
        {
            var characters = new Character[]
            {
                new Character
                {
                    Name = "Tyrion Lannister", 
                    Age = 30,
                    Description = "Младший сын лорда Тайвина Ланнистера. Он — карлик, поэтому вынужден использовать свой интеллект, чтобы преодолеть предубеждения других людей относительно себя. За свой рост Тирион получил прозвище «Полумуж», а за острый язык — «Бес».",
                    IsAlive = true,
                    IsMan = true,
                    Price = 5,
                    ImageName = "Tyrion_Lannister.jpg"
                },
                new Character
                {
                    Name = "Arya Stark", 
                    Age = 14,
                    Description = "Mладшая дочь лорда Эддарда Старка из Винтерфелла и его жены Кейтилин Старк. У неё есть четыре брата и сестра. Арья совсем не похожа на Сансу: она независима, не желает в будущем стать леди и вышиванию предпочитает обучение владению оружием.",
                    IsAlive = true,
                    IsMan = false,
                    Price = 3,
                    ImageName = "Arya_Stark.png"
                },
                new Character 
                {
                    Name = "Petyr Baelish", 
                    Age = 42,
                    Description = "Бывший мастер над монетой и член Малого совета при короле, позже за свои заслуги он получил замок Харренхол, и стал номинальным правителем Речных земель. Опытный политик, который умеет манипулировать людьми для получения собственной выгоды. Владеет несколькими борделями в Королевской Гавани.",
                    IsAlive = true,
                    IsMan = true,
                    Price = 4,
                    ImageName = "Petyr_Baelish.png"
                },
                new Character
                {
                    Name = "Sandor Clegane", 
                    Age = 40,
                    Description = "Личный телохранитель Джоффри Баратеона. Он молчаливый, ожесточенный и часто грубит, но может проявить сострадание. Его лицо обезображено страшным шрамом от ожога, покрывающим правую половину лица. Эту травму в детстве нанёс ему его родной брат Григор Клиган.",
                    IsAlive = false,
                    IsMan = true,
                    Price = 1,
                    ImageName = "Sandor_Clegane.png"
                },
            };
            context.Characters.AddRange(characters);
            context.SaveChanges();
        }
    }
}