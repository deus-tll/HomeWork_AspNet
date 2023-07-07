using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApp.Pages
{
    public class Task5Model : PageModel
    {
        private List<Author> _authors = new();
        public Author? CurrentAuthor { get; set; }

        public Task5Model()
        {
            FillAuthors();
        }

        public void OnGet()
        {
            int authorId = new Random().Next(0, _authors.Count);
            CurrentAuthor = _authors[authorId];
        }

        private void FillAuthors ()
        {
            _authors.Add(new Author { Id = 1, FullName = "Matt Murdock", Quotes = new List<string>
            {
                "I’m not seeking penance for what I’ve done, father. I’m asking forgiveness for what I’m about to do.",
                "You don’t get to destroy who I am.",
                "How do you know the angel and the devil inside me aren’t the same thing?"
            } });

            _authors.Add(new Author
            {
                Id = 1,
                FullName = "Albert Einstein",
                Quotes = new List<string>
            {
                "Two things are infinite: the universe and human stupidity; and I'm not sure about the universe.",
                "I am enough of an artist to draw freely upon my imagination. Imagination is more important than knowledge. Knowledge is limited. Imagination encircles the world.",
                "There are only two ways to live your life. One is as though nothing is a miracle. The other is as though everything is a miracle.",
                "Reality is merely an illusion, albeit a very persistent one."
            }
            });

            _authors.Add(new Author
            {
                Id = 1,
                FullName = "Friedrich Nietzsche",
                Quotes = new List<string>
            {
                "Without music, life would be a mistake.",
                "That which does not kill us makes us stronger."
            }
            });

            _authors.Add(new Author
            {
                Id = 1,
                FullName = "Socrates",
                Quotes = new List<string>
            {
                "The only true wisdom is in knowing you know nothing.",
                "To find yourself, think for yourself.",
                "He who is not contented with what he has, would not be contented with what he would like to have.",
                "Sometimes you put walls up not to keep people out, but to see who cares enough to break them down."
            }
            });

            _authors.Add(new Author
            {
                Id = 1,
                FullName = "Bruce Wayne",
                Quotes = new List<string>
            {
                "The world only makes sense if you force it to.",
                "It's not who I am underneath, but what I do that defines me."
            }
            });

            _authors.Add(new Author
            {
                Id = 1,
                FullName = "Satoru Gojo",
                Quotes = new List<string>
            {
                "Perception, communication... every action associated with living is forcibly repeated an infinite number of times. It's ironic, isn't it? When granted everything, you can't do anything.",
                "Dying to win and risking death to win are completely different, Megumi.",
                "But no matter how many allies you have around you, when you die, you'll be alone.",
                "I believe love is the most twisted curse of all."
            }
            });
        }
    }

    public class Author
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public List<string> Quotes { get; set; }
    }
}
