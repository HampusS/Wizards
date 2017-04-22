using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wizards.GameObjects;

namespace Wizards.Utilities
{
    class Calculate
    {


        public static bool CheckCircleCollision(GameObject lhs, GameObject rhs)
        {
            float totalRadius = lhs.getRadius() + rhs.getRadius();
            Vector2 deltaPos = rhs.myPosition - lhs.myPosition;
            return totalRadius * totalRadius > (deltaPos.X * deltaPos.X) + (deltaPos.Y * deltaPos.Y);
        }

        public static Vector2 CalculateCollisionNormal(Vector2 posA, Vector2 posB)
        {
            Vector2 normal = posB - posA;
            return Vector2.Normalize(normal);
        }

        public static void GiveImpulse(MovingObject reciever, float strength)
        {
            Vector2 direction = new Vector2((float)Math.Cos(reciever.getAngle()), (float)Math.Sin(reciever.getAngle()));
            reciever.myVelocity += direction * strength;
        }

        public static void SolveToStaticCircleCollision(MovingObject obj_moving, GameObject obj_static)
        {
            float totalRadius = obj_moving.getRadius() + obj_static.getRadius();
            Vector2 collisionNormal = CalculateCollisionNormal(obj_static.myPosition, obj_moving.myPosition);

            obj_moving.myPosition = obj_static.myPosition + (collisionNormal * (totalRadius + 0.001f));
            obj_moving.myVelocity = Vector2.Reflect(obj_moving.myVelocity, collisionNormal) * obj_static.getRestitution();
        }

        public static void SolveToMovingCircleCollision(MovingObject lhs, MovingObject rhs)
        {
            Vector2 deltaPos = Vector2.Subtract(lhs.myPosition, rhs.myPosition);
            float distance = deltaPos.Length();
            float invMassLhs = 1 / lhs.getMass();
            float invMassRhs = 1 / rhs.getMass();

            // Move the two objects away from collision
            Vector2 midTransDistance = Vector2.Multiply(deltaPos, (((lhs.getRadius() + rhs.getRadius()) - distance) / distance) + 0.001f);
            lhs.myPosition = Vector2.Add(lhs.myPosition, midTransDistance);
            rhs.myPosition = Vector2.Subtract(rhs.myPosition, midTransDistance);

            // Give new velocity
            Vector2 relVel = Vector2.Subtract(lhs.myVelocity, rhs.myVelocity);
            midTransDistance.Normalize();
            float vn = Vector2.Dot(relVel, midTransDistance);
            if (vn <= 0.0)
            {
                float i = (-(1.0f + (lhs.getRestitution() + rhs.getRestitution()) * vn) / (invMassLhs + invMassRhs));
                Vector2 impulse = Vector2.Multiply(midTransDistance, i);
                lhs.myVelocity += Vector2.Multiply(impulse, invMassLhs);
                rhs.myVelocity -= Vector2.Multiply(impulse, invMassRhs);
            }
        }
    }
}
