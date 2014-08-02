﻿// Copyright (c) Aura development team - Licensed under GNU GPL
// For more information, see license file in the main folder

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Aura.Channel.Util;
using Aura.Channel.World.Entities;
using Aura.Channel.Scripting;

namespace Aura.Channel.Database
{
	public class Account
	{
		public string Id { get; set; }
		public long SessionKey { get; set; }

		public int Authority { get; set; }

		public DateTime LastLogin { get; set; }

		public string BanReason { get; set; }
		public DateTime BanExpiration { get; set; }

		public List<Character> Characters { get; set; }
		public List<Pet> Pets { get; set; }

		public ScriptVariables Vars { get; protected set; }

		public Account()
		{
			this.Characters = new List<Character>();
			this.Pets = new List<Pet>();
			this.Vars = new ScriptVariables();

			this.LastLogin = DateTime.Now;
		}

		public PlayerCreature GetCharacterOrPet(long entityId)
		{
			PlayerCreature result = this.Characters.FirstOrDefault(a => a.EntityId == entityId);
			if (result == null)
				result = this.Pets.FirstOrDefault(a => a.EntityId == entityId);
			return result;
		}

		public PlayerCreature GetCharacterOrPetSafe(long entityId)
		{
			var r = this.GetCharacterOrPet(entityId);
			if (r == null)
				throw new SevereViolation("Account doesn't contain character 0x{0:X}", entityId);

			return r;
		}

		public Pet GetPet(long entityId)
		{
			return this.Pets.FirstOrDefault(a => a.EntityId == entityId);
		}

		public Pet GetPetSafe(long entityId)
		{
			var r = this.GetPet(entityId);
			if (r == null)
				throw new SevereViolation("Account doesn't contain pet 0x{0:X}", entityId);

			return r;
		}
	}
}
