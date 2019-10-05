﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Assets.SF.UI.Wrap
{
    public class ButtonWrapper : BaseWrapper<Button>, IBindCommand
    {
        private readonly Button button;

        public ButtonWrapper(Button _button) : base(_button)
        {
            button = _button;
        }

        public UnityEvent GetBindCommandFunc()
        {
            return button.onClick;
        }
    }
}
