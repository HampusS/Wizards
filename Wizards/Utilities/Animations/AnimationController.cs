using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wizards.GameObjects;
using Wizards.Utilities.Animations;

namespace Wizards.Utilities
{
    class AnimationController
    {
        Wizard m_goAgent;
        Animation walking;

        public AnimationController(Wizard agent)
        {
            m_goAgent = agent;
            walking = new Walk(agent);
        }

        public void Update(float time)
        {
            walking.Update(time);
        }

    }
}
