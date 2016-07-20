using AbideChallenge.Data;
using AbideChallenge.Data.Abstract;
using AbideChallenge.Data.Concrete;
using AbideChallenge.Questions.Abstract;
using AbideChallenge.Questions.Concrete;
using AbideChallenge.Repositories.Abstract;
using AbideChallenge.Repositories.Concrete;
using Ninject;
using System;
using System.Web.Mvc;
using System.Web.Routing;

namespace AbideChallenge.Infrastructure
{
    public class NinjectControllerFactory : DefaultControllerFactory
    {
        private IKernel ninjectKernel;

        public NinjectControllerFactory()
        {
            ninjectKernel = new StandardKernel();
            AddBindings();
        }

        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            return controllerType == null ? null : (IController)ninjectKernel.Get(controllerType);
        }

        private void AddBindings()
        {
            ninjectKernel.Bind<IPracticeRepository>().To<PracticeRepository>();
            ninjectKernel.Bind<IPrescriptionRepository>().To<PrescriptionRepository>();
            ninjectKernel.Bind<ICSVReader>().To<CSVReader>();
            ninjectKernel.Bind<IPostcodeHelper>().To<PostcodeHelper>();
            ninjectKernel.Bind<IQuestionService>().To<QuestionService>();
        }
    }
}
