using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace XIDE.Core
{
	/// <summary>
	/// A generic Singleton which can be inherited from to allow only a single instance of a class
	/// to exist at any given time.
	/// Reference: http://blog.paranoidferret.com/index.php/2007/06/22/csharp-tutorial-singleton-pattern/
	/// </summary>
	/// <typeparam name="T">The derived class type to allow only a single instance of.</typeparam>
	public class Singleton<T> where T : class, new()
	{
		#region Private Fields
		
		/// <summary>
		/// The single instance of type T
		/// </summary>
		private static T mInstance;
		
		#endregion


		#region Public Properties

		/// <summary>
		/// Determine if an instance for the given type has been created.
		/// </summary>
		public static bool HasInstance
		{
			get { return mInstance != null; }
		}

		#endregion


		#region Singleton Usage

		/// <summary>
		/// Get the global instance of the derived type.
		/// </summary>
		/// <returns>The global object if it has been created. An exception is thrown otherwise</returns>
		public static T GetInstance()
		{
			if( mInstance == null )
			{
				throw new InvalidOperationException("Singleton of type " + typeof(T).Name + " must be created before usage.");
			}

			Debug.Assert(mInstance != null, "Singleton not created for type " + typeof(T).FullName + ".");
			return mInstance;
		}


		/// <summary>
		/// Creates the global instance. This can only be called once and must be called before 'GetInstance'
		/// </summary>
		public static void Create()
		{
			if( mInstance != null )
			{
				throw new InvalidOperationException("Singleton already created for type " + typeof(T).FullName + ".");
			}
			mInstance = new T();
		}


		/// <summary>
		/// Destroys the global object. If the object is referenced elsewhere it will stay in existence
		/// but the Singleton will be available to create a different instance
		/// </summary>
		public static void Destroy()
		{
			if( mInstance == null )
			{
				throw new InvalidOperationException("No singleton of type " + typeof(T).FullName + " exists to destroy");
			}
			mInstance = null;
		}

		#endregion
	}
}
