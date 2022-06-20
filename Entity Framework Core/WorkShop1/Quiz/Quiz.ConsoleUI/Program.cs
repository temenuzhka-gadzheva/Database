using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Quiz.Data;
using Quiz.Services;
using Quiz.Services.Interfaces;
using System;
using System.IO;

namespace Quiz.ConsoleUI
{
    public class Program
    {
        static void Main()
        {
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);
            var serviceProvider = serviceCollection.BuildServiceProvider();

            /* var dbContext = serviceProvider.GetService<ApplicationDbContext>();

             // принтираме всички потребители
             foreach (var item in dbContext.Users)
             {
                 Console.WriteLine(item.UserName);
             }*/

            // var quizService = serviceProvider.GetService<IQuizService>();
            //quizService.Add("C# DB");

            /* var quiz = quizService.GetQuizById(1);

             Console.WriteLine(quiz.Title);

             foreach (var question in quiz.Questions)
             {
                 Console.WriteLine(question.Title);
                 foreach (var answer in question.Answers)
                 {
                     Console.WriteLine(answer.Title);
                 }
             }*/

             /* var questionService = serviceProvider.GetService<IQuestionService>();
              questionService.Add("How seasons have movie Castle?",1);

             var answerService = serviceProvider.GetService<IAnswerService>();
            answerService.Add("9",5,true,2);

             var userAnswerService = serviceProvider.GetService<IUserAnswerService>();
             userAnswerService.AddUserAnswer("3e865df8-c91e-4f39-9512-9ed26bdeee7b", 1,2,1);*/

            var quizService = serviceProvider.GetService<IUserAnswerService>();
            var quiz = quizService.GetUserResult("3e865df8-c91e-4f39-9512-9ed26bdeee7b", 1);

            Console.WriteLine(quiz);
        }

        // контейнер за регистрация на сървиси, на контекст 
        private static void ConfigureServices(IServiceCollection services)
        {
            var config = new ConfigurationBuilder()
                // взима настоящата директория
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(config.GetConnectionString("DefaultConnection")));

            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationDbContext>();

            // всеки път когато то бъде извиквано ще се създава нова инстанция към него 
            // services.AddTransient();

            services.AddTransient<IQuizService, QuizService>();
            services.AddTransient<IQuestionService, QuestionService>();
            services.AddTransient<IAnswerService, AnswerService>();
            services.AddTransient<IUserAnswerService, UserAnswerService>();
        }
    }
}
