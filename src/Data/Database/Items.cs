﻿// Copyright (c) Aura development team - Licensed under GNU GPL
// For more information, see license file in the main folder

using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace Aura.Data.Database
{
	[Serializable]
	public class ItemData : TaggableData
	{
		public int Id { get; set; }

		public string Name { get; set; }
		public string KorName { get; set; }

		public ItemType Type { get; set; }

		/// <summary>
		/// Specifies whether an item is consumed upon use.
		/// </summary>
		public bool Consumed { get; set; }

		public byte Width { get; set; }
		public byte Height { get; set; }

		public byte ColorMap1 { get; set; }
		public byte ColorMap2 { get; set; }
		public byte ColorMap3 { get; set; }

		public StackType StackType { get; set; }
		public ushort StackMax { get; set; }
		public int StackItem { get; set; }
		public int Price { get; set; }
		public int SellingPrice { get; set; }
		public int Durability;

		public int Defense { get; set; }
		public short Protection { get; set; }

		public byte WeaponType { get; set; }
		public InstrumentType InstrumentType { get; set; }

		public short Range { get; set; }
		public ushort AttackMin { get; set; }
		public ushort AttackMax { get; set; }
		public byte Critical { get; set; }
		public byte Balance { get; set; }
		public byte AttackSpeed { get; set; }
		public byte KnockCount { get; set; }

		public int BagWidth { get; set; }
		public int BagHeight { get; set; }

		public string OnUse { get; set; }
		public string OnEquip { get; set; }
		public string OnUnequip { get; set; }
		public string OnCreation { get; set; }
	}

	/// <summary>
	/// Item database, indexed by item id.
	/// </summary>
	public class ItemDb : DatabaseJsonIndexed<int, ItemData>
	{
		public ItemData Find(string name)
		{
			name = name.ToLower();
			return this.Entries.FirstOrDefault(a => a.Value.Name.ToLower() == name).Value;
		}

		public List<ItemData> FindAll(string name)
		{
			name = name.ToLower();
			return this.Entries.FindAll(a => a.Value.Name.ToLower().Contains(name));
		}

		protected override void ReadEntry(JObject entry)
		{
			entry.AssertNotMissing("id", "name", "originalName", "tags", "type", "width", "height", "price");

			var info = new ItemData();
			info.Id = entry.ReadInt("id");

			info.Name = entry.ReadString("name");
			info.KorName = entry.ReadString("originalName");
			info.Tags = entry.ReadString("tags");
			info.Type = (ItemType)entry.ReadInt("type");
			info.StackType = (StackType)entry.ReadInt("stackType");
			info.StackMax = entry.ReadUShort("stackMax", 1);

			if (info.StackMax < 1)
				info.StackMax = 1;

			info.StackItem = entry.ReadInt("stackItem");

			info.Consumed = entry.ReadBool("consumed");
			info.Width = entry.ReadByte("width");
			info.Height = entry.ReadByte("height");
			info.ColorMap1 = entry.ReadByte("colorMap1");
			info.ColorMap2 = entry.ReadByte("colorMap2");
			info.ColorMap3 = entry.ReadByte("colorMap3");
			info.Price = entry.ReadInt("price");
			info.SellingPrice = (info.Id != 2000 ? (int)(info.Price * 0.1f) : 1000);
			info.Durability = entry.ReadInt("durability");
			info.Defense = entry.ReadInt("defense");
			info.Protection = entry.ReadShort("protection");
			info.InstrumentType = (InstrumentType)entry.ReadInt("instrumentType");

			info.WeaponType = entry.ReadByte("weaponType");
			if (info.WeaponType != 0)
			{
				info.Range = entry.ReadShort("range");
				info.AttackMin = entry.ReadUShort("attackMin");
				info.AttackMax = entry.ReadUShort("attackMax");
				info.Critical = entry.ReadByte("critical");
				info.Balance = entry.ReadByte("balance");
				info.AttackSpeed = entry.ReadByte("attackSpeed");
				info.KnockCount = entry.ReadByte("knockCount");
			}

			info.BagWidth = entry.ReadInt("bagWidth");
			info.BagHeight = entry.ReadInt("bagHeight");

			info.OnUse = entry.ReadString("onUse");
			info.OnEquip = entry.ReadString("onEquip");
			info.OnUnequip = entry.ReadString("onUnequip");
			info.OnCreation = entry.ReadString("onCreation");

			this.Entries[info.Id] = info;
		}
	}

	public enum ItemType
	{
		Armor = 0,
		Headgear = 1,
		Glove = 2,
		Shoe = 3,
		Book = 4,
		Currency = 5,
		ItemBag = 6,
		Weapon = 7,
		Weapon2H = 8, // 2H, bows, tools, etc
		Weapon2 = 9, // Ineffective Weapons? Signs, etc.
		Instrument = 10,
		Shield = 11,
		Robe = 12,
		Accessory = 13,
		SecondaryWeapon = 14,
		MusicScroll = 15,
		Manual = 16,
		EnchantScroll = 17,
		CollectionBook = 18,
		ShopLicense = 19,
		FaliasTreasure = 20,
		Kiosk = 21,
		StyleArmor = 22,
		StyleHeadgear = 23,
		StyleGlove = 24,
		StyleShoe = 25,
		ComboCard = 27,
		Unknown2 = 28,
		Hair = 100,
		Face = 101,
		Usable = 501,
		Quest = 502,
		Usable2 = 503,
		Unknown1 = 504,
		Sac = 1000,
		Misc = 1001,
	}

	public enum StackType
	{
		None = 0,
		Stackable = 1,
		Sac = 2,
	}

	public enum InstrumentType
	{
		Lute = 0,
		Ukulele = 1,
		Mandolin = 2,
		Whistle = 3,
		Roncadora = 4,
		Flute = 5,
		Chalumeau = 6,

		ToneBottleC = 7,
		ToneBottleD = 8,
		ToneBottleE = 9,
		ToneBottleF = 10,
		ToneBottleG = 11,
		ToneBottleB = 12,
		ToneBottleA = 13,

		Tuba = 18,
		Lyra = 19,
		ElectricGuitar = 20,

		Piano = 21,
		Violin = 22,
		Cello = 23,

		BassDrum = 66,
		Drum = 67,
		Cymbals = 68,

		HandbellC = 69,
		HandbellD = 70,
		HandbellE = 71,
		HandbellF = 72,
		HandbellG = 73,
		HandbellB = 74,
		HandbellA = 75,
		HandbellHighC = 76,

		Xylophone = 77,

		MaleVoiceKr1 = 81,
		MaleVoiceKr2 = 82,
		MaleVoiceKr3 = 83,
		MaleVoiceKr4 = 84,
		FemaleVoiceKr1 = 90,
		FemaleVoiceKr2 = 91,
		FemaleVoiceKr3 = 92,
		FemaleVoiceKr4 = 93,
		FemaleVoiceKr5 = 94,

		MaleChorusVoice = 100,
		FemaleChorusVoice = 110,

		MaleVoiceJp = 120,
		FemaleVoiceJp = 121,
	}
}
