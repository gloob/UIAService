﻿/*
 * The R&D leading to these results received funding from the
 * Department of Education - Grant H421A150005 (GPII-APCP). However,
 * these results do not necessarily represent the policy of the
 * Department of Education, and you should not assume endorsement by the
 * Federal Government.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UIALib
{
    public delegate Event ReactSrc();
    public delegate Event EventSinker(Event e);

    /// <summary>
    /// Base type with the 'mininum component info' for construction
    /// </summary>
    public class Component
    {
        /// <summary>
        /// Name for component identification.
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// Type of the component, we need to decide which will be a good, "set of
        /// values" for this type, as we don't know how many possible "types" of
        /// components we want, or even if this distinction is valuable.
        /// </summary>
        public string type { get; set; }
    }

    /// <summary>
    /// Non reactive "Listener Component", that "listen to event" but isn't a
    /// source of reactivity.
    /// </summary>
    public class LComponent : Component
    {
        /// <summary>
        /// The components can listen to the events of other components, those events,
        /// needs to be fully matchable with its component source. Neither
        /// this channel creation, or the target is responsability of each component.
        /// This inversion of control allows us to instanciate components without any
        /// need or reference of "who" will listen.
        /// </summary>
        public List<Event> lEvents { get; set; }
        /// <summary>
        /// Each component should be able to consume the events to which he is subscriber,
        /// and each one is responsible for its implementation.
        /// </summary>
        public EventSinker sinker { get; set; }
    }

    /// <summary>
    /// This represents a reactive component. Maybe in the future, we can avoid
    /// the use of "path" when the full 'work-load' of finding a valid component
    /// could fall in the match-making. But right now, for speed needs, we are
    /// going to fully specify the path walkers need to follow, to reach a component.
    /// The inner list of 'Components' a component have, describe the "Expansion tree"
    /// of components that should be done in a reactive way. It's describe the child
    /// components in the UI Automation tree that expands from this particular one.
    /// And that should be observed when this particular one is instanciated.
    /// </summary>
    public class RComponent : Component
    {
        /// <summary>
        /// Source of component reactivity.
        /// </summary>
        public ReactSrc eventSrc { get; set; }
    }

    public class LRComponent : Component
    {
        /// <summary>
        /// Source of component reactivity.
        /// </summary>
        public ReactSrc eventSrc { get; set; }
        /// <summary>
        /// Each component should be able to consume the events to which he is subscriber,
        /// and each one is responsible for its implementation.
        /// </summary>
        public EventSinker sinker { get; set; }
    }
}
