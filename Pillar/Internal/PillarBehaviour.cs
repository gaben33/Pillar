using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pillar3D {
	public abstract class PillarBehaviour : Component {
		public PillarBehaviour() : base(false) {
			base.Update += Update;
			Container.OnLevelChanged += Rebase;
			Start();
		}

		public abstract void Start();

		new public abstract void Update();

		private void Rebase() => Container.ContainerLevel.Rail.Update -= Update;
	}
}
