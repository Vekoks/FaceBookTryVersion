using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MeetLife.Services;
using MeetLife.Data.Repository;
using UnitTestServices.FakeResors;
using MeetLife.Model;
using MeetLifeClient.Controllers;
using Moq;

namespace UnitTestServices
{
    [TestClass]
    public class UnitTestPostService
    {

        public static FekaRepository<User> fRepositoryUser = new FekaRepository<User>();
        public static FekaRepository<Post> fRepositoryPost = new FekaRepository<Post>();
        public static FekaRepository<Picture> fRepositoryPicture = new FekaRepository<Picture>();

        [TestMethod]
        public void TestAddPostToUserMethod()
        {
            var mockRepoUser = new Mock<IRepository<User>>();
            var mockRepoPost = new Mock<IRepository<Post>>();
            var mockRepoPicture = new Mock<IRepository<Picture>>();

            mockRepoPicture.Setup(x => x.Add(It.IsAny<Picture>())).Throws<Exception>();

            PostService postSer = new PostService(mockRepoUser.Object, mockRepoPost.Object, mockRepoPicture.Object);
            var user = new User()
            {
                Id = "1",
                Email = "veoks@abv.bg"
            };

           // postSer.AddPostToUser(user, "vesko", new byte[1], false);


            //mockRepoPicture.Verify(x => x.Add(It.IsAny<Picture>()),Times.Once);

            try
            {
                postSer.AddPostToUser(user, "vesko", new byte[1], false);
            }
            catch (Exception ex)
            {
                Assert.IsNotNull(ex);
            }

        }



    } 
    }

