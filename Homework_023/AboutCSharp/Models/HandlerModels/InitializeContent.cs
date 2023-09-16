using AboutCSharp.Models.DataModels;

namespace AboutCSharp.Models.HandlerModels
{
    public static class InitializeContent
    {
        public static List<ContentBlock> InitializeIndexContent()
        {
            return new List<ContentBlock>()
            {
                new ContentBlock()
                {
                    Header = "What is C# Programming? A Beginner's Guide",

                    TextContent = "C# is a popular language for a variety of reasons, but mainly because it's multi-paradigm " +
                                  "language that is versatile, fairly easy to learn and object-oriented. With so many different programming " +
                                  "languages to choose from, we thought we’d give you a proper introduction to C# so you can decide if it’s " +
                                  "the right fit for your next project."
                }
            };
        }


        public static List<ContentBlock> InitializeAboutContent()
        {
            return new List<ContentBlock>()
            {
                new ContentBlock()
                {
                    Header = "What Is C#?",

                    TextContent = "C# is a modern, general-purpose programming language that can be used to perform a wide " +
                    "range of tasks and objectives that span over a variety of professions. C# is primarily used on the " +
                    "Windows .NET framework, although it can be applied to an open source platform. This highly versatile " +
                    "programming language is an object-oriented programming language (OOP) and comparably new to the game, " +
                    "yet a reliable crowd pleaser."
                },

                new ContentBlock()
                {
                    Header = "When was C# created?",

                    TextContent = "When compared to long-standing languages like Python and PHP, C# is a young addition to " +
                    "the programming family at nearly twenty years old. The language was developed in the year 2000 by " +
                    "Microsoft’s Anders Hejlsberg, a Danish software engineer with a history for popular creations. " +
                    "Anders has taken part in the creation of a handful of dependable programming tools and languages, " +
                    "including Microsoft’s TypeScript and Delphi, a suitable replacement for Turbo Pascal." +
                    "<br class=\"line-break\">" +
                    "As of November 2022, C# ranked #4 on the PYPL Popularity of Programming Language Index, right behind Java and JavaScript. " +
                    "The data used to compile this index is based on how often people search for a tutorial on different " +
                    "programming languages in Google." +
                    "<br class=\"line-break\">" +
                    "C# has also made a consistent appearance in the top ten programming " +
                    "languages in the TIOBE Index, a report that pulls its data from a compilation of popular search engines " +
                    "including Google, YouTube and Bing."
                },

                new ContentBlock()
                {
                    Header = "Where did C# get its name?",

                    TextContent = "In the beginning, C# was originally titled COOL, a clever acronym that stood for " +
                    "“C-like Object Oriented Language.” Unfortunately, Microsoft was unable to hang onto the fun name for " +
                    "reasons having to do with trademark law." +
                    "<br class=\"line-break\">" +
                    "C# was originally designed to rival Java. Judging by the " +
                    "quick rise to popularity and the positive response from both new and seasoned developers, it’s safe to " +
                    "say that goal has been achieved."
                }
            };
        }


        public static List<ContentBlock> InitializeAdvantagesContent()
        {
            return new List<ContentBlock>()
            {
                new ContentBlock()
                {
                    Header = "C# programming can save you time",

                    TextContent = "Perhaps the greatest advantage is how much time you can save by using C# instead of a " +
                    "different programming language. Being that C# is statically typed and easy to read, users can expect " +
                    "to spend less time scouring their scripts for tiny errors that disrupt the function of the application." +
                    "<br class=\"line-break\">" +
                    "C# also emphasizes simplicity and efficiency, so programmers can spend less time writing complicated stacks " +
                    "of code that are repeatedly used throughout the project. Top it all off with an extensive memory bank and " +
                    "you’ve got a time-effective language that can easily reduce labor hours and help you meet tight deadlines " +
                    "without tossing back that third cup of coffee at 2:00am."
                },

                new ContentBlock()
                {
                    Header = "C# is easy to learn",

                    TextContent = "In addition to the time you can save during project development, you’ll also spend less time " +
                    "learning C# as opposed to the more difficult programming languages out there. Thanks to its simplicity and " +
                    "easy-to-use features, C# offers a fairly low learning curve for beginners." +
                    "<br class=\"line-break\">" +
                    "This language makes for a great " +
                    "first step into the field and provides aspiring developers with a comfortable way to become familiar with " +
                    "programming without becoming discouraged and overwhelmed."
                },

                new ContentBlock()
                {
                    Header = "C# is scalable and easy to maintain",

                    TextContent = "C# is a programming language that is remarkably scalable and easy to maintain. Because " +
                    "of the strict nature of how static codes must be written, C# programs are reliably consistent, which makes " +
                    "them much easier to adjust and maintain than programs that are written using other languages." +
                    "<br class=\"line-break\">" +
                    "If you ever " +
                    "need to return to an old project written in C#, you’ll be pleased to find that while your processes may " +
                    "have changed over the years, your C# stack has remained the same across the board. There is a place for " +
                    "everything and everything is in its place."
                },

                new ContentBlock()
                {
                    Header = "C# is object-oriented",

                    TextContent = "C# is completely object-oriented, which is a rare characteristic for a programming language. " +
                    "Many of the most common languages incorporate object orientation to an extent, but very few have accomplished " +
                    "the magnitude of C# without losing favor from the people." +
                    "<br class=\"line-break\">" +
                    "There are many different advantages to object-oriented " +
                    "programming (or OOP), such as efficiency and flexibility to name a few. Some developers who are unfamiliar " +
                    "with OOP may feel a little reluctant to choose a new language with such a heavy emphasis on it, but don’t " +
                    "worry: understanding object-oriented programming isn’t all that difficult."
                },

                new ContentBlock()
                {
                    Header = "C# has a great community",

                    TextContent = "In the world of coding and programming, the importance of a helpful community on which you can depend " +
                    "simply can’t be overstated. Programming languages aren’t a platform or service with a dedicated help line or convenient " +
                    "IT support. Programmers must rely on the support of others in the same field who have experienced the same roadblocks " +
                    "and frustrations." +
                    "<br class=\"line-break\">" +
                    "One such community of helpful programming experts can be found on StackOverflow. Because this Q&A " +
                    "site was constructed in C#, it’s no surprise that C# developers make up a massive portion of the community where you " +
                    "can go to ask, answer, brainstorm, or vent." +
                    "<br class=\"line-break\">" +
                    "If you prefer to collaborate with like-minded individuals face-to-face, " +
                    "C# also has an extensive community on Meetup.com, where members can join both online and IRL discussions that are " +
                    "scheduled at random or on a consistent basis."
                }
            };
        }


        public static List<ContentBlock> InitializeAreasContent()
        {
            return new List<ContentBlock>()
            {
                new ContentBlock()
                {
                    Header = "What is C# used for?",

                    TextContent = "Like other general-purpose programming languages, C# can be used to create a number of " +
                    "different programs and applications: mobile apps, desktop apps, cloud-based services, " +
                    "websites, enterprise software and games. Lots and lots of games. While C# is remarkably " +
                    "versatile, there are three areas in which it is most commonly used."
                },

                new ContentBlock()
                {
                    Header = "Windows applications",

                    TextContent = "C# was created by Microsoft for Microsoft, so it’s easy to see why it’s most popularly used for the " +
                    "development of Windows desktop applications. C# applications require the Windows .NET framework in order to function " +
                    "at their best, so the strongest use case for this language is developing applications and programs that are specific " +
                    "to the architecture of the Microsoft platform."
                },

                new ContentBlock()
                {
                    Header = "C# for Games",

                    TextContent = "C# might just be one of the best programming languages for gaming. This language is heavily " +
                    "used to create fan-favorite games like Rimworld on the Unity Game Engine." +
                    "<br class=\"line-break\">" +
                    "Just in case you weren’t already " +
                    "aware, Unity is by far the most popular game engine available, on which more than a third of the industry’s " +
                    "best and most commonly-played games were built. C# integrates seamlessly with the Unity engine and can be used " +
                    "on virtually any modern mobile device or console thanks to cross-platform tech like Xamarin."
                },

                new ContentBlock()
                {
                    Header = "C# for website development",

                    TextContent = "C# is often used to develop professional, dynamic websites on the .NET platform, or open-source software. " +
                    "So, even if you’re not a fan of the Microsoft architecture, you can still use C# to create a fully-functional website. " +
                    "Because this language is object-oriented, it is often utilized to develop websites that are incredibly efficient, easily " +
                    "scalable and a breeze to maintain."
                }
            };
        }


        public static List<RefBlock> InitializeAppsRefsList()
        {
            return new List<RefBlock>
            {
                new RefBlock()
                {
                    Name = "Microsoft Visual Studio",
                    Ref = "https://visualstudio.microsoft.com/"
                },

                new RefBlock()
                {
                    Name = "Paint.NET",
                    Ref = "https://www.getpaint.net/"
                },

                new RefBlock()
                {
                    Name = "Open Dental",
                    Ref = "https://www.opendental.com/"
                },

                new RefBlock()
                {
                    Name = "KeePass",
                    Ref = "https://keepass.info/"
                },

                new RefBlock()
                {
                    Name = "FlashDevelop",
                    Ref = "https://www.flashdevelop.org/"
                },

                new RefBlock()
                {
                    Name = "NMath",
                    Ref = "https://www.centerspace.net/nmath"
                },

                new RefBlock()
                {
                    Name = "Pinta",
                    Ref = "https://www.pinta-project.com/"
                },

                new RefBlock()
                {
                    Name = "OpenRA",
                    Ref = "https://www.openra.net/"
                },
            };
        }
    }
}
