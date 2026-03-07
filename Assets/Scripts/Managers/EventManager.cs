using System;
using System.Collections;

/// <summary>
/// Handles the management and invocation of custom events between objects.
/// Provides methods for listening to events, stopping listening, and invoking events.
/// </summary>
public class EventManager
{

	#region Fields

	// Singleton instance of the EventManager
	private static EventManager instance;

	// Stores event mappings using a hashtable for quick lookup
	private readonly Hashtable eventHash = new();

	#endregion

	#region Private Methods

	/// <summary>
	/// Generates a unique key for storing events with generic arguments
	/// </summary>
	/// <typeparam name="T">The type of the event argument</typeparam>
	/// <param name="eventType">The type of the custom event</param>
	/// <returns>A unique string key for identifying the event in the hash table</returns>
	private static string GetKey<T>(CustomEventType eventType)
	{
		Type type = typeof(T);
		return type + eventType.ToString(); ;
	}

	#endregion

	#region Public Methods

	/// <summary>
	/// Initializes the EventManager instance
	/// </summary>
	public static void Initialize()
	{
		instance = new();
	}

	/// <summary>
	/// Registers a listener for a generic event type
	/// </summary>
	/// <typeparam name="T">The type of event argument</typeparam>
	/// <param name="eventType">The type of the custom event to listen for</param>
	/// <param name="listener">The callback method to execute when the event is invoked</param>
	public static void StartListening<T>(CustomEventType eventType, Action<T> listener)
	{
		string key = GetKey<T>(eventType);

		if (instance.eventHash.ContainsKey(key))
		{
			Action<T> thisEvent = (Action<T>)instance.eventHash[key];
			thisEvent += listener;
			instance.eventHash[key] = thisEvent;
		}
		else
		{
			instance.eventHash.Add(key, listener);
		}
	}

	/// <summary>
	/// Registers a listener for an event without arguments
	/// </summary>
	/// <param name="eventType">The type of the custom event to listen for</param>
	/// <param name="listener">The callback method to execute when the event is invoked</param>
	public static void StartListening(CustomEventType eventType, Action listener)
	{
		if (instance.eventHash.ContainsKey(eventType))
		{
			Action thisEvent = (Action)instance.eventHash[eventType];
			thisEvent += listener;
			instance.eventHash[eventType] = thisEvent;
		}
		else
		{
			instance.eventHash.Add(eventType, listener);
		}
	}

	/// <summary>
	/// Stops listening to a specific generic event type
	/// </summary>
	/// <typeparam name="T">The type of the event argument</typeparam>
	/// <param name="eventType">The type of the custom event</param>
	/// <param name="listener">The callback method to remove</param>
	public static void StopListening<T>(CustomEventType eventType, Action<T> listener)
	{
		string key = GetKey<T>(eventType);

		if (instance.eventHash.ContainsKey(key))
		{
			Action<T> thisEvent = (Action<T>)instance.eventHash[key];
			thisEvent -= listener;

			if (thisEvent == null)
			{
				instance.eventHash.Remove(key);
			}
			else
			{
				instance.eventHash[key] = thisEvent;
			}
		}
	}

	/// <summary>
	/// Stops listening to a specific event without arguments
	/// </summary>
	/// <param name="eventType">The type of the custom event</param>
	/// <param name="listener">The callback method to remove</param>
	public static void StopListening(CustomEventType eventType, Action listener)
	{
		if (instance.eventHash.ContainsKey(eventType))
		{
			Action thisEvent = (Action)instance.eventHash[eventType];
			thisEvent -= listener;

			if (thisEvent == null)
			{
				instance.eventHash.Remove(eventType);
			}
			else
			{
				instance.eventHash[eventType] = thisEvent;
			}
		}
	}

	/// <summary>
	/// Invokes a generic event with an argument
	/// </summary>
	/// <typeparam name="T">The type of the event argument</typeparam>
	/// <param name="eventType">The type of the custom event</param>
	/// <param name="value">The value to pass to the event listener</param>
	public static void Invoke<T>(CustomEventType eventType, T value)
	{
		string key = GetKey<T>(eventType);

		if (instance.eventHash.ContainsKey(key))
		{
			Action<T> thisEvent = (Action<T>)instance.eventHash[key];
			thisEvent?.Invoke(value);
		}
	}

	/// <summary>
	/// Invokes an event without arguments
	/// </summary>
	/// <param name="eventType">The type of the custom event</param>
	public static void Invoke(CustomEventType eventType)
	{
		if (instance.eventHash.ContainsKey(eventType))
		{
			Action thisEvent = (Action)instance.eventHash[eventType];
			thisEvent?.Invoke();
		}
	}

	/// <summary>
	/// A generic helper method to reduce repetition of "listen, invoke, stop-listening" event calls
	/// </summary>
	/// <typeparam name="T">The argument type associated with the event callback</typeparam>
	/// <param name="requestEventType">The event to invoke that triggers the response event</param>
	/// <param name="responseEventType">The event that will carry the callback data</param>
	/// <param name="callback">The method that processes the callback data</param>
	public static void RequestEventWithCallback<T>(CustomEventType requestEventType, CustomEventType responseEventType, Action<T> callback)
	{
		// Listen Once
		StartListening<T>(responseEventType, (data) =>
		{
			callback(data);
			StopListening<T>(responseEventType, callback);
		});

		// Trigger the request
		Invoke(requestEventType);
	}

	#endregion

}