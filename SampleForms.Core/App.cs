using MvvmCross.Platform.IoC;

namespace SampleForms.Core
{
	public class App : MvvmCross.Core.ViewModels.MvxApplication
	{
		public override void Initialize()
		{
			CreatableTypes()
				.EndingWith("Service")
				.AsInterfaces()
				.RegisterAsLazySingleton();

			RegisterAppStart<ViewModels.FirstViewModel>();
		}
	}
}
