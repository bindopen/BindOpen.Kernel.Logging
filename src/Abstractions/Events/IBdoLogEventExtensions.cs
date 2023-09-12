﻿namespace BindOpen.Kernel.Logging.Events
{
    /// <summary>
    /// This static class provides extensions to BdoLogEvent class.
    /// </summary>
    public static class IBdoLogEventExtensions
    {
        /// <summary>
        /// Updates the log of the specified object..
        /// </summary>
        /// <param name="ev">The event to consider.</param>
        /// <param name="log">The log to consider</param>
        /// <returns>Returns the specified object.</returns>
        public static T WithLog<T>(
            this T ev,
            IBdoLog log)
            where T : IBdoLogEvent
        {
            if (ev != null)
            {
                ev.Log = log as IBdoDynamicLog;
            }

            return ev;
        }

        /// <summary>
        /// Updates the parent of the specified object..
        /// </summary>
        /// <param name="ev">The event to consider.</param>
        /// <param name="parent">The parent log to consider.</param>
        /// <returns>Returns the specified object.</returns>
        public static T WithParent<T>(
            this T ev,
            IBdoDynamicLog parent)
            where T : IBdoLogEvent
        {
            if (ev != null)
            {
                ev.Parent = parent;
            }

            return ev;
        }


        /// <summary>
        /// Updates the result code of the specified object..
        /// </summary>
        /// <param name="ev">The event to consider.</param>
        /// <param name="resultCode">The result code to consider.</param>
        /// <returns>Returns the specified object.</returns>
        public static T WithResultCode<T>(
            this T ev,
            string resultCode)
            where T : IBdoLogEvent
        {
            if (ev != null)
            {
                ev.ResultCode = resultCode;
            }

            return ev;
        }


        /// <summary>
        /// Updates the source of the specified object..
        /// </summary>
        /// <param name="ev">The event to consider.</param>
        /// <param name="source">The source to consider.</param>
        /// <returns>Returns the specified object.</returns>
        public static T WithSource<T>(
            this T ev,
            string source)
            where T : IBdoLogEvent
        {
            if (ev != null)
            {
                ev.Source = source;
            }

            return ev;
        }


        /// <summary>
        /// Updates the stack traces of the specified object..
        /// </summary>
        /// <param name="ev">The event to consider.</param>
        /// <param name="stackTraces">The stack traces to consider.</param>
        /// <returns>Returns the specified object.</returns>
        public static T WithStackTraces<T>(
            this T ev,
            params IBdoLogEventStackTrace[] stackTraces)
            where T : IBdoLogEvent
        {
            if (ev != null)
            {
                ev.StackTraces = stackTraces;
            }

            return ev;
        }

        /// <summary>
        /// Gets the maximum kind of the events of the specified object.
        /// </summary>
        /// <param name="ev">The event to consider.</param>
        /// <param name="isRecursive">Indicate whether the search is recursive.</param>
        /// <param name="kinds">The kinds to consider.</param>
        /// <returns>True if this instance has the specified events. False otherwise.</returns>
        public static EventKinds GetMaxEventKind<T>(
            this T ev,
            bool isRecursive = true,
            params EventKinds[] kinds)
            where T : IBdoLogEvent
        {
            EventKinds eventKind = EventKinds.None;

            if (ev?.Log != null)
            {
                eventKind = ev.Log.GetMaxEventKind(isRecursive, kinds);
            }

            if (eventKind == EventKinds.None)
            {
                eventKind = ev?.Kind ?? EventKinds.None;
            }

            return eventKind;
        }
    }
}