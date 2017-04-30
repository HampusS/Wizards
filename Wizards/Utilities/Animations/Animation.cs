using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wizards.GameObjects;

namespace Wizards.Utilities
{
    class Animation
    {
        public Wizard m_goAgent;

        protected bool isActive;

        public Animation(Wizard agent)
        {
            m_goAgent = agent;
            isActive = false;
        }

        public virtual void Update(float time)
        {

        }

    }
}
