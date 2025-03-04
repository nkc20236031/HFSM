using System.Collections.Generic;

namespace RizeLibrary.StateMachine
{
	public class Parameter<T>
	{
		private readonly Dictionary<T, object> _parameters = new();

		/// <summary>
		/// パラメータの設定
		/// </summary>
		/// <param name="key">キー</param>
		/// <param name="value">初期値</param>
		/// <typeparam name="TValue">型</typeparam>
		public void Add<TValue>(T key, TValue value)
		{
			_parameters.Add(key, value);
		}
		
		/// <summary>
		/// パラメータの取得
		/// </summary>
		/// <param name="key">キー</param>
		/// <typeparam name="TValue">型</typeparam>
		public TValue Get<TValue>(T key)
		{
			if (_parameters.TryGetValue(key, out object parameter))
			{
				return (TValue)parameter;
			}

			return default;
		}
		
		/// <summary>
		/// パラメータの設定
		/// </summary>
		/// <param name="key">キー</param>
		/// <param name="value">値</param>
		/// <typeparam name="TValue">型</typeparam>
		public void Set<TValue>(T key, TValue value)
		{
			if (_parameters.ContainsKey(key))
			{
				_parameters[key] = value;
			}
		}
	}
}
