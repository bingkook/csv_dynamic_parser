using System;
using System.Collections.Generic;
using System.Linq;
using Unity;
using Unity.Resolution;

namespace CSVDynamicParser.Core
{
	public class InjectContainer
	{
		private static IUnityContainer unityContainer;

		static InjectContainer()
		{
			unityContainer = new UnityContainer();
		}

		public static void RegisterType<TFrom, TTo>() where TTo : TFrom
		{
			unityContainer.RegisterType<TFrom, TTo>();
		}

		public static void RegisterType(Type from, Type to)
		{
			unityContainer.RegisterType(from, to);
		}

		public static void RegisterInstance<T>(T o)
		{
			unityContainer.RegisterInstance(typeof(T), o);
		}

		public static void RegisterInstance(Type t, object instance)
		{
			unityContainer.RegisterInstance(t, instance);
		}

		public static T GetInstance<T>()
		{
			return unityContainer.Resolve<T>();
		}

		public static T GetInstance<T>(Dictionary<string, object> parameters)
		{
			var parameterOverrides = parameters.Select(p => new ParameterOverride(p.Key, p.Value)).ToArray();

			return unityContainer.Resolve<T>(parameterOverrides);
		}
	}
}