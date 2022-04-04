using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class Animal : Entity
{
    private const float BIRTH_COOLDOWN = 0.35f;

    public CircleCollider2D ProximityCollider;
    private List<Entity> closeEntities;

    private float _lastBirthTime;
    private int _frameCountOffset;

    private float _hitForce;

    private float _hitSpeed;
    private Vector3 _hitDirection;
    private Vector3 _idleDirection;

    [SerializeField]private AnimalState _currentState;

    protected override void Awake()
    {
        base.Awake();

        closeEntities = new List<Entity>();

        _lastBirthTime = 0;
        _frameCountOffset = UnityEngine.Random.Range(0, 10);

        _hitSpeed = 0;
        _hitDirection = Vector3.zero;

        _currentState = AnimalState.Idle;
    }

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        if (this.IsAlive())
        {
            if ((Time.frameCount + _frameCountOffset) % 10 == 0)
            {
                // Refresh close entities list using colliders
                this.ScanCloseEntities();
            }

            this.UpdateIdleDirection();

            this.GrowIfRequired();
            this.ManageNormalLife();
        }
    }

    private void UpdateIdleDirection()
    {
        if (Random.Range(0f, 1f) < 1f / 200f)
        {
            _idleDirection = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0).normalized;
        }
    }

    private void ScanCloseEntities()
    {
        closeEntities = _collisionsToolkit.GetCloseEntities(transform, ProximityCollider);
    }

    public void RemoveCloseEntity(Entity entity)
    {
        closeEntities.Remove(entity);
    }

    private void ManageNormalLife()
    {
        Vector3 oldPosition = transform.position;
        
        if (closeEntities.Count > 0)
        {
            List<Entity> closePredators = this.ExtractClosePredators(closeEntities);
            List<Entity> closeFellows = this.ExtractCloseFellows(closeEntities);
            List<Entity> closePreys = this.ExtractClosePreys(closeEntities);

            if (closePredators.Count > 0)
            {
                this.Flee(closePredators);
            }
            else if (closeFellows.Count > 0 && this.HasEnoughVitalityToReproduce())
            {
                if(!this.Reproduce(closeFellows))
                {
                    this.Idle();
                }
            }
            else if (closePreys.Count > 0)
            {
                if (!this.Eat(closePreys))
                {
                    this.Idle();
                }

            } else
            {
                this.Idle();
            }
            //Entity closest = ExtractClosestEntity(closeEntities);
            Entity closest = ExtractClosestAnimal(closeEntities);
            if (closest != null && this.IsTouchingEntity(closest))
            {
                float totalRadius = closest.HitboxCollider.radius + HitboxCollider.radius;
                Vector3 distanceFromEntity = (transform.position - closest.transform.position);
                transform.position = closest.transform.position + (distanceFromEntity.normalized * totalRadius);
            }
        }
        else
        {
            this.Idle();
        }
        this.ManageHit();

        Collider2D wall = Physics2D.OverlapPoint(transform.position, 1 << 6);
        
        if (wall != null)
        {
            Vector3 newPosition = new Vector3(transform.position.x, oldPosition.y, transform.position.z);
            wall = Physics2D.OverlapPoint(newPosition, 1 << 6);
            if (wall == null)
            {
                transform.position = newPosition;
            }
            else
            {
                newPosition = new Vector3(oldPosition.x, transform.position.y, transform.position.z);
                wall = Physics2D.OverlapPoint(newPosition, 1 << 6);
                if (wall == null)
                {
                    transform.position = newPosition;
                }
                else
                {
                    transform.position = oldPosition;
                }
            }
        }
    }


    private void ManageHit()
    {
        if (_hitSpeed > 0 || _hitForce > 0)
        {

            _hitSpeed = _hitForce;
            Vector3 hitVelocity = _hitDirection * -_hitSpeed;

            Debug.Log(_id + "  " + hitVelocity.magnitude);
            this.Move(hitVelocity);

            _hitForce = Math.Max(_hitForce - AnimalPreset.CollideDrag, 0);
        }
    }

    private bool CanSee(Entity other)
    {
        RaycastHit2D d = Physics2D.Linecast(this.transform.position, other.transform.position, 1 << 6);
        return d.collider == null;
    }

    private void Flee(List<Entity> closePredators)
    {
        Entity closestPredator = this.ExtractClosestEntity(closePredators);
        
        Vector3 feeDelta = -(closestPredator.transform.position - transform.position);
        this.Move(feeDelta);

        _currentState = AnimalState.Fleeing;
    }

    private bool Reproduce(List<Entity> closeFellows)
    {
        Entity closestFellow = this.ExtractClosestEntity(closeFellows);
        if (!CanSee(closestFellow))
        {
            return false;
        }
        Vector3 reproduceDelta = (closestFellow.transform.position - transform.position);
        this.Move(reproduceDelta);

        bool canGiveBirth = ((Animal)closestFellow).CanGiveBirth();

        if (this.IsTouchingEntity(closestFellow) && canGiveBirth && Id.CompareTo(closestFellow.Id) > 0)
        {
            Vector3 center = (transform.position + closestFellow.transform.position) / 2f;
            this.PublishBirth(center);

            this.OffsetVitality(-AnimalPreset.ReproductionCost);
            closestFellow.OffsetVitality(-closestFellow.Preset.ReproductionCost);
            
            _lastBirthTime = Time.fixedTime;
		}

        _currentState = AnimalState.Reproducing;
        return true;
    }

    public bool CanGiveBirth()
    {
        return Time.fixedTime >= _lastBirthTime + BIRTH_COOLDOWN;
    }

    private bool Eat(List<Entity> closePreys)
    {
        Entity closestPrey = this.ExtractYummyestEntity(closePreys);
        if (!CanSee(closestPrey))
        {
            return false;
        }

        Vector3 eatDelta = (closestPrey.transform.position - transform.position);
        this.Move(eatDelta);

        if (this.IsTouchingEntity(closestPrey) && closestPrey.CanTakeHit())
        {
            Vector3 preyDelta = (closestPrey.transform.position - transform.position);
            
            float effectiveDamage = Math.Min(AnimalPreset.Power, closestPrey.Vitality);
            bool isDead = closestPrey.TakeHit(effectiveDamage, -preyDelta.normalized);
            
            float vitalityGain = closestPrey.Preset.NutritionalValue * (effectiveDamage / closestPrey.Preset.MaxVitality);
            this.OffsetVitality(Math.Min(vitalityGain, AnimalPreset.MaxVitality));
        }

        _currentState = AnimalState.Eating;
        return true;
    }

    public bool IsTouchingEntity(Entity otherEntity)
    {
        float entitiesDistance = Vector3.Distance(transform.position, otherEntity.transform.position);
        return entitiesDistance <= Preset.CollideRadius + otherEntity.Preset.CollideRadius;
    }

    public override bool TakeHit(float damage, Vector3 hitDirection)
    {
        bool isDead = base.TakeHit(damage, hitDirection);

        if (!isDead)
        {
            _hitForce = AnimalPreset.CollideBounce * 20;
            _hitDirection = hitDirection;
            _hitSpeed = 0;

            return isDead;
        }
        else
        {
            return true;
        }
    }

    protected override bool IsAlive()
    {
        return _currentState != AnimalState.Dead;
    }
    
    protected override void Die()
    {
        _currentState = AnimalState.Dead;
        this.PublishDeath();
    }
    
    private void Idle()
    {
        _currentState = AnimalState.Idle;
        this.Move(_idleDirection);
    }

    protected override void Move(Vector3 delta)
    {
        Vector3 direction = delta.normalized;
        float speed = Math.Min(delta.magnitude, AnimalPreset.MoveSpeed);
        base.Move(direction * speed);
        _mainSprite.sortingOrder = (int)transform.position.y+50;
    }


    private List<Entity> ExtractClosePreys(List<Entity> entities)
    {
        return new List<Entity>(entities.FindAll((entity) => AnimalPreset.Preys.Contains(entity.Type)));
    }

    private List<Entity> ExtractCloseFellows(List<Entity> entities)
    {
        return new List<Entity>(entities.FindAll((entity) => Type == entity.Type));
    }

    private List<Entity> ExtractClosePredators(List<Entity> entities)
    {
        return new List<Entity>(entities.FindAll((entity) => AnimalPreset.Predators.Contains(entity.Type)));
    }

    private Entity ExtractClosestEntity(List<Entity> entities)
    {
        if (entities.Count == 0)
        {
            return null;
        }

        return entities.OrderBy((entity) => Vector3.Distance(transform.position, entity.transform.position)).First();
    }
    private Entity ExtractYummyestEntity(List<Entity> entities)
    {
        if (entities.Count == 0)
        {
            return null;
        }

        return entities.OrderBy((entity) => -entity.Preset.NutritionalValue/ Vector3.Distance(transform.position, entity.transform.position)).First();
    }
    private Entity ExtractClosestAnimal(List<Entity> entities)
    {
        if (entities.Count == 0)
        {
            return null;
        }
        IEnumerable<Entity> animals = entities.Where((entity) => entity is Animal);
        if (animals.Count() == 0) return null;
        return animals.OrderBy((entity) => Vector3.Distance(transform.position, entity.transform.position)).First();
    }

    private bool HasEnoughVitalityToReproduce()
    {
        return Vitality >= AnimalPreset.ReproductionThreshold * AnimalPreset.MaxVitality;
    }

    private AnimalPreset AnimalPreset
    {
        get
        {
            return (AnimalPreset)Preset;
        }
    }
}
