﻿namespace Graph.Compatibility {
	/// <summary>
	/// Will behave as if all node item connectors are compatible.
	/// </summary>
	public class AlwaysCompatible : ICompatibilityStrategy {
		/// <summary>
		/// Determine if two node item connectors could be connected to each other.
		/// </summary>
		/// <param name="from">From which node connector are we connecting.</param>
		/// <param name="to">To which node connector are we connecting?</param>
		/// <returns><see langword="true"/> if the connection is valid; <see langword="false"/> otherwise</returns>
		public bool CanConnect( NodeConnector from, NodeConnector to )
		{
			return true;
		}
	}
}
