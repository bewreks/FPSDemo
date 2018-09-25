using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FPSDemo
{
	public abstract class BaseView<M> : BaseModelContainer<M> where M : BaseModel
	{
		protected IEnumerator WaitForAnimation ( Animation animation )
		{
			do
			{
				yield return null;
			} while ( animation.isPlaying );
		}
	}
}
