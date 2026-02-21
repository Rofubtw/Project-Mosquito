using UnityEngine;

namespace Extensions
{
	public static class RigidbodyExt
	{
		public static void ResetVelocity(this Rigidbody self)
		{
			self.linearVelocity = Vector3.zero;
		}

		public static void StopMovement(this Rigidbody self)
		{
			self.linearVelocity = Vector3.zero;
			self.angularVelocity = Vector3.zero;
		}

		public static void SetKinematic(this Rigidbody self,bool isKinematic)
		{
			self.isKinematic = isKinematic;		
		}

		public static void SetConstraints(this Rigidbody self, RigidbodyConstraints constraints)
		{
			self.constraints = constraints;
		}

		public static void SetConstraints(this Rigidbody self, params RigidbodyConstraints[] numbers)
		{
			self.SetConstraints(RigidbodyConstraints.None);

			for (int i = 0; i < numbers.Length; i++)
			{
				self.constraints |= numbers[i];
			}
		}

		public static void RemoveConstraints(this Rigidbody self, params RigidbodyConstraints[] numbers)
		{

			for (int i = 0; i < numbers.Length; i++)
			{
				self.constraints &= ~numbers[i];
			}
		}

		public static float KineticEnergy(Rigidbody self)
		{
			return 0.5f * self.mass * self.linearVelocity.sqrMagnitude;
		}
	}
}
