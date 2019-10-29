// Copyright (c) Daniel Crenna & Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;

namespace Blowdart.UI.Instructions
{
    public abstract class EditorInstruction : RenderInstruction
    {
        public Type Type { get; protected set; }

        public override string DebuggerDisplay => $"Editor<{Type.Name}>";
        public object Object { get; protected set; }
    }

    public class EditorInstruction<T> : EditorInstruction
    {
        private readonly T _instance;

        public EditorInstruction(T instance)
        {
            _instance = instance;
            Type = _instance.GetType();
            Object = _instance;
        }
    }
}
