using NUnit.Framework;
using FoodDiary.Services.EnergyValueCalculator;
using FoodDiary.Data;
using FoodDiary.ViewModels.Food;
using Moq;
using AutoMapper;
using FoodDiary.Data.Entities;

namespace FoodDiaryTests.Services.EnergyValueCalculator
{
    [TestFixture]
    public class EnergyValueCalculatorServiceTests
    {
        private ResourseSpecificationViewModel? resourseSpecification;

        [SetUp]
        public void SetUp()
        {
            resourseSpecification = new ResourseSpecificationViewModel()
            {
                Id = 0,
                Composition = new List<CompositionItemViewModel>(),
                OutputDishWeightG = 1000,
                DishValue = new DishValueViewModel() { Id = 1 }
                
            };

            resourseSpecification.Composition
                .Add(new CompositionItemViewModel()
                    {
                        Id = 0,
                        Product = new Product()
                        {
                            Id = 0,
                            Calories = 77,
                            Protein = 2,
                            Fat = 0.4,
                            Carbohydrate = 16.3
                        },
                        ProductWeightG= 400,
                    });
            resourseSpecification.Composition
                .Add(new CompositionItemViewModel()
                {
                    Id = 0,
                    Product = new Product()
                    {
                        Id = 0,
                        Calories = 54,
                        Protein = 2.9,
                        Fat = 2.5,
                        Carbohydrate = 4.8
                    },
                    ProductWeightG = 100,
                });

            resourseSpecification.Composition
                .Add(new CompositionItemViewModel()
                {
                    Id = 0,
                    Product = new Product()
                    {
                        Id = 0,
                        Calories = 661,
                        Protein = 0.8,
                        Fat = 72.5,
                        Carbohydrate = 1.3
                    },
                    ProductWeightG = 35,
                });
        }

        [Test]
        public void CalculateDishValue_ThreeSetProducts_ReturnCurrentDishValue()
        {
           
            var mockResourseSpecificationViewModel = new Mock<ResourseSpecificationViewModel>();

            var mockRepository = new Mock<IMyAppRepository>();
            var mockMapper = new Mock<IMapper>();


            EnergyValueCalculatorService energyValueCalculator = new EnergyValueCalculatorService(mockRepository.Object, mockMapper.Object);

            DishValueViewModel expectedDishValue = new DishValueViewModel()
            {
                Id = 1,
                Calories = 59.34, 
                Protein = 1.12,
                Fat = 2.95,
                Carbohydrate = 7.05
            };


            var dishValue = energyValueCalculator.CalculateDishValue(resourseSpecification);

            Assert.Multiple(() =>
            {
                Assert.That(dishValue.Id, Is.EqualTo(expectedDishValue.Id));
                Assert.That(dishValue.Calories, Is.EqualTo(expectedDishValue.Calories));
                Assert.That(dishValue.Protein, Is.EqualTo(expectedDishValue.Protein));
                Assert.That(dishValue.Fat, Is.EqualTo(expectedDishValue.Fat));
                Assert.That(dishValue.Carbohydrate, Is.EqualTo(expectedDishValue.Carbohydrate));
            });           


        }   



    }
}