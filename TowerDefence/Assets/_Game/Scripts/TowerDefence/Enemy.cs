using UnityEngine;
using System.Collections;

namespace TowerDefence {

	public class Enemy : MonoBehaviour {

		public delegate void DelegateDestroy( Enemy enemy );
		public event DelegateDestroy EventDied;
		public event DelegateDestroy EventReachedTarget;

		private Health health;
		private HitFlash hitFlash;
		private int startingHealth;

		//===================================================
		// UNITY METHODS
		//===================================================

		/// <summary>
		/// Awake.
		/// </summary>
		void Awake() {
			health = GetComponent<Health>();
			hitFlash = GetComponent<HitFlash>();
			startingHealth = health.HealthValue;			
		}

		/// <summary>
		/// Start.
		/// </summary>
		void Start() {
		}

		/// <summary>
		/// Update.
		/// </summary>
		void Update() {

		}

		/// <summary>
		/// Called when [enable].
		/// </summary>
		void OnEnable() {
			health.SetStartingHealth( startingHealth );
		}

		/// <summary>
		/// Called when a the trigger is fired.
		/// </summary>
		/// <param name="collider">The collider.</param>
		void OnTriggerEnter( Collider collider ) {
			if( collider.gameObject.tag == Tags.Projectile ) {

				// apply the projectile damage.
				Projectile projectile = collider.gameObject.transform.parent.gameObject.GetComponent<Projectile>();
				health.Damage( projectile.Damage );

				// destroy the collidee - projectile
				Destroy( collider.gameObject.transform.parent.gameObject );
				
				hitFlash.Flash();

				// check health
				if( health.HealthValue <= 0 ) {
					Died();
				}
			} else if ( collider.gameObject.tag == Tags.Goal ) {
				ReachedTarget();
			}
		}

		//===================================================
		// PUBLIC METHODS
		//===================================================

		/// <summary>
		/// Sets the health value.
		/// </summary>
		/// <param name="value">The value.</param>
		public void SetHealthValue( int value ) {
			health.SetStartingHealth( value );
		}

		//===================================================
		// PRIVATE METHODS
		//===================================================

		/// <summary>
		/// Called when there is no health.
		/// </summary>
		private void Died() {
			if( EventDied != null ) {
				EventDied( this );
			}
		}

		/// <summary>
		/// Called when it reacheds the target.
		/// </summary>
		private void ReachedTarget() {
			if( EventReachedTarget != null ) {
				EventReachedTarget( this );
			}
		}

		//===================================================
		// EVENTS METHODS
		//===================================================


	}
}