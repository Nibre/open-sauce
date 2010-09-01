/*
    Yelo: Open Sauce SDK
		Halo 1 (CE) Edition
    Copyright (C) 2005-2010  Kornner Studios (http://kornner.com)

    This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program.  If not, see <http://www.gnu.org/licenses/>.
*/


namespace Yelo
{
	namespace Objects
	{
		namespace Vehicle
		{
#if PLATFORM_IS_DEDI
			void Initialize()	{}
			void Dispose()		{}
#else

			struct vehicle_globals {
				TextBlock* menu;

				int32 presets_count;
				struct s_seat_preset
				{
					int32 index;
					real_vector3d offset;
				};
				struct s_preset {
					char name[Enums::k_vehicle_view_name_length];
					uint32 seats_bitvector[BIT_VECTOR_SIZE_IN_DWORDS(Enums::k_vehicle_view_max_seat_presets)];
					s_seat_preset seats[Enums::k_vehicle_view_max_seat_presets];
				}presets[Enums::k_vehicle_view_max_vehicle_presets];


				void Dispose()
				{
					if(menu != NULL)
					{
						delete menu;
						menu = NULL;
					}
				}

				s_preset* AddPreset(cstring name)
				{
					if(presets_count == Enums::k_vehicle_view_max_vehicle_presets-1)
						return NULL;

					s_preset* preset = &presets[presets_count++];
					strcpy_s(preset->name, NUMBEROF(preset->name), name);

					return preset;
				}

			private:
				s_unit_datum* GetCurrentVehicle(int16* current_seat_index)
				{
					datum_index player_index = Players::LocalPlayerIndex();
					s_unit_datum* vehicle = Players::GetVehicle(player_index, current_seat_index);

					return vehicle ? vehicle : NULL;
				}

			public:

			}_vehicle_globals;

			void Initialize()
			{
			}

			void Dispose()
			{
				_vehicle_globals.Dispose();
			}

			bool AdjustSettings()
			{
				_vehicle_globals.menu->Render();

				return false;
			}

			void LoadSettings(TiXmlElement* vehicles_element)
			{
			}

			void SaveSettings(TiXmlElement* vehicles_element)
			{
			}

#if defined(DX_WRAPPER)
			void Initialize3D(IDirect3DDevice9 *pDevice, D3DPRESENT_PARAMETERS *pPP)
			{
				_vehicle_globals.menu = new TextBlock(pDevice,pPP);
				_vehicle_globals.menu->ApplyScheme();
				_vehicle_globals.menu->SetDimensions(350,0);
				_vehicle_globals.menu->Attach(Enums::_attach_method_center, 0,0,0,0);
				_vehicle_globals.menu->SetTextAlign(DT_CENTER);

// 				_vehicle_globals.menu->SetText(
// 					L"Mouse \x2195 = Distance\n"
// 					L"Shift+Mouse \x21BA = Axis\n"
// 					L"\n"
// 					L"Left-Click to Save.\n"
// 					L"Right-Click to Reset.");
				_vehicle_globals.menu->SetText(L"I don't think so, Scooter :|");
				_vehicle_globals.menu->Refresh();
			}

			void OnLostDevice()
			{
				_vehicle_globals.menu->OnLostDevice();
			}

			void OnResetDevice(D3DPRESENT_PARAMETERS *pPP)
			{
				_vehicle_globals.menu->OnResetDevice(pPP);
				_vehicle_globals.menu->Refresh();
			}

			void Render() {}

#elif PLATFORM_IS_USER
			// TODO: Need fallback initialization for when we're not using the DX_WRAPPER
#endif

#endif
		};
	};
};