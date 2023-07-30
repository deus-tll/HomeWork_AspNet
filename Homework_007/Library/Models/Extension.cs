namespace Library.Models
{
    public static class Extension
    {
        public static List<Book> CreateListOfDefaultBooks()
        {
            List<Book> books = new()
            {
                new Book()
                {
                    Title = "The Catcher in the Rye",
                    Description = "It's Christmas time and Holden Caulfield has just been expelled from yet another school...\n" +
                    "Fleeing the crooks at Pencey Prep, he pinballs around New York City seeking solace in fleeting encounters—shooting " +
                    "the bull with strangers in dive hotels, wandering alone round Central Park, getting beaten up by pimps and cut down " +
                    "by erstwhile girlfriends. The city is beautiful and terrible, in all its neon loneliness and seedy glamour, " +
                    "its mingled sense of possibility and emptiness. Holden passes through it like a ghost, thinking always of " +
                    "his kid sister Phoebe, the only person who really understands him, and his determination to escape the phonies " +
                    "and find a life of true meaning.",

                    Author = "Jerome David Salinger",
                    Publisher = "Penguin",
                    PageCount = 240,
                    Genre = "Realistic fiction, Coming-of-age fiction",
                    Year = 2018
                },

                new Book()
                {
                    Title = "Flowers for Algernon",
                    Description = "Charlie Gordon, a floor sweeper born with an unusually low IQ, has been " +
                    "chosen as the perfect subject for an experimental surgery that doctors hope will increase " +
                    "his intelligence - a procedure that has been highly successful when tested on a lab mouse " +
                    "named Algernon. All Charlie wants is to be smart and have friends, but the treatement turns " +
                    "him into a genius.\nThen Algernon begins to fade. What will become of Charlie?",
                    Author = "Daniel Keyes",
                    Publisher = "Weidenfeld & Nicolson",
                    PageCount = 256,
                    Genre = "Science fiction",
                    Year = 2017
                },

                new Book()
                {
                    Title = "To Kill a Mockingbird",
                    Description = "Lawyer Atticus Finch gives this advice to his children as he defends the real " +
                    "mockingbird of Harper Lee's classic novel - a black man charged with the rape of a white girl. " +
                    "Through the young eyes of Scout and Jem Finch, Harper Lee explores with exuberant humour the " +
                    "irrationality of adult attitudes to race and class in the Deep South of the 1930s. " +
                    "The conscience of a town steeped in prejudice, violence and hypocrisy is pricked by " +
                    "the stamina of one man's struggle for justice. But the weight of history will only tolerate so much...",
                    Author = "Harper Lee",
                    Publisher = "Random House",
                    PageCount = 320,
                    Genre = "Southern Gothic, Bildungsroman",
                    Year = 1989
                },

                new Book()
                {
                    Title = "Martin Eden",
                    Description = "It is about a young sailor who falls in love with a girl from a wealthy family. " +
                    "The hero of the work is trying to become a writer in order to find his own place in life. " +
                    "The work is, in a sense, autobiographical.",
                    Author = "Jack London",
                    Publisher = "Macmillan",
                    PageCount = 393,
                    Genre = "Künstlerroman",
                    Year = 1909
                },

                new Book()
                {
                    Title = "The Dawn of Day",
                    Description = "Edited by a noted Nietzsche scholar, this authoritative compendium is a vital " +
                    "assembly of nearly all of Nietzsche's early works. Marking the advent of his mature philosophy, " +
                    "these aphorisms and prose poems examine the impulses that lead human beings to seek the comforts " +
                    "of religion, morality, metaphysics, and art. Nietzsche proposes greater individualism and personality " +
                    "development, addresses issues of society and family, and discusses visions of free spirits with " +
                    "the courage to be rid of idealist prejudices. Written in his distinctive, often paradoxical style, " +
                    "The Dawn of Day presents practically every theme touched upon in Nietzsche's later philosophical " +
                    "essays. It is an essential guide and a fundamental basis for the understanding of the great philosopher and his work.",
                    Author = "Friedrich Nietzsche",
                    Publisher = "CreateSpace Independent Publishing Platform",
                    PageCount = 116,
                    Genre = "Philosophy",
                    Year = 2017
                },

                new Book()
                {
                    Title = "The Little Prince",
                    Description = "After crash-landing in the Sahara Desert, a pilot encounters a little prince who " +
                    "is visiting Earth from his own planet. Their strange and moving meeting illuminates for the " +
                    "aviator many of life's universal truths, as he comes to learn what it means to be human from " +
                    "a child who is not.",
                    Author = "Antoine de Saint-Exupery",
                    Publisher = "Macmillan Ltd",
                    PageCount = 136,
                    Genre = "Children's literature, Fable, Novella, Speculative fiction",
                    Year = 2016
                },

                new Book()
                {
                    Title = "Love Lasts Three Years",
                    Description = "The former dandy Marc Marronnier divorces Anne after three years of marriage. " +
                    "He has fallen in love with Alice and tries to convince her to leave her husband.",
                    Author = "Frederic Beigbeder",
                    Publisher = "Editions Grasset",
                    PageCount = 233,
                    Genre = "Love novel",
                    Year = 1997
                },

                new Book()
                {
                    Title = "Submission",
                    Description = "As the 2022 French Presidential election looms, two candidates emerge as favourites: " +
                    "Marine Le Pen of the Front National, and the charismatic Muhammed Ben Abbes of the growing Muslim " +
                    "Fraternity. Forming a controversial alliance with the political left to block the Front National’s " +
                    "alarming ascendency, Ben Abbes sweeps to power, and overnight the country is transformed. This proves " +
                    "to be the death knell of French secularism, as Islamic law comes into force: women are veiled, polygamy " +
                    "is encouraged and, for our narrator François – misanthropic, middle-aged and alienated – life is set on a new course.",
                    Author = "Michel Houellebecq",
                    Publisher = "Random House",
                    PageCount = 256,
                    Genre = "Novel, Fiction",
                    Year = 2016
                },

                new Book()
                {
                    Title = "The Conspiracy Against the Human Race: A Contrivance of Horror",
                    Description = "The Conspiracy against the Human Race is renowned horror writer Thomas Ligotti's first work of nonfiction. " +
                    "Through impressively wide-ranging discussions of and reflections on literary and philosophical works of a pessimistic " +
                    "bent, he shows that the greatest horrors are not the products of our imagination. The worst and most plentiful horrors " +
                    "are instead to be found in reality. Mr. Ligotti's calm, but often bloodcurdling turns of phrase, evoke the dreadfulness " +
                    "of the human condition. Those who cannot bear the truth will pretend this is another work of fiction, but in doing so " +
                    "they perpetuate the conspiracy of the book's title.",
                    Author = "Thomas Ligotti",
                    Publisher = "Viking Press",
                    PageCount = 245,
                    Genre = "Non-fiction",
                    Year = 2010
                },

                new Book()
                {
                    Title = "Critique Of Pure Reason",
                    Description = "The most accurate and informative English translation of Kant's most important " +
                    "philosophical work in both the 1781 and 1787 editions; faithful rendering of Kant's terminology, " +
                    "syntax, and sentence structure; a simple and direct style suitable for readers at all levels; " +
                    "distinct versions of all those portions of the work substantially revised by Kant for the 1787 " +
                    "edition; all Kant's handwritten emendations and marginal notes from his own personal copy reproduced " +
                    "for the first time in any edition, German or English; a large-scale introduction providing a " +
                    "summary of the structure and arguments of the Critique as well as the most informative account " +
                    "available in English of its long and complex genesis; and an extensive editorial apparatus " +
                    "including informative annotation and glossaries.",
                    Author = "Immanuel Kant",
                    Publisher = "Everyman",
                    PageCount = 560,
                    Genre = "Philosophy",
                    Year = 1998
                }
            };

            return books;
        }
    }
}
