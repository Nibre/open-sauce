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
#include "Common/Precompile.hpp"
#include "Networking/GameSpyApi.hpp"

#include "Memory/MemoryInterface.hpp"
#include "Game/GameState.hpp"

namespace Yelo
{
#define __EL_INCLUDE_ID			__EL_INCLUDE_NETWORKING
#define __EL_INCLUDE_FILE_ID	__EL_NETWORKING_GAMESPY_API
#include "Memory/_EngineLayout.inl"

	namespace Networking
	{
		s_gamespy_socket* GsSocket()										DPTR_IMP_GET(gs_Socket);
		s_gamespy_socket* GsLoopbackSocket()								DPTR_IMP_GET(gs_LoopbackSocket);
		s_gamespy_config* GsConfig()										PTR_IMP_GET2(gamespy_config);
		s_gamespy_globals* GsGlobals()										PTR_IMP_GET2(gamespy_globals);
#if PLATFORM_IS_USER											// While the getter for this is only in User builds, I keep the definition around just for documentation's sake
		s_gamespy_server_browser_globals* GsServerBrowserGlobals()			PTR_IMP_GET2(gamespy_server_browser_globals);
#endif

		s_gamespy_product* GsProducts()										PTR_IMP_GET2(gamespy_products_list);

		API_FUNC_NAKED s_gamespy_client* GsGetClient(int32 client_id)
		{
			static uint32 TEMP_CALL_ADDR = GET_FUNC_PTR(GAMESPY_GET_CLIENT_KEY_HASH);

			__asm {
				push	ebp
				mov		ebp, esp
				push	ecx

				mov		eax, client_id
				call	TEMP_CALL_ADDR
				cmp		eax, GET_DATA_PTR(compiler_null_string)
				jnz		_return
				mov		eax, 4 // will cause us to return NULL. Yes, I r a hacka
				
_return:
				sub		eax, 4

				pop		ecx
				pop		ebp
				retn	4 * 1
			}
		}
	};


	namespace Enums
	{
		enum game_version_id : long_enum {
			//_game_version_id_ = ,
			_game_version_id_100 = 0x94ECF,	//  609999
			_game_version_id_107 = 0x5BC42F,// 6013999
			_game_version_id_108 = 0x5BCFE7,// 6016999
			_game_version_id_109 = 0x96A27,	//  616999
		};
	};

	namespace Networking
	{
		static void ChangeAdvertisedGameVersionId(long_enum version_id, bool and_game_build)
		{
			if(version_id)
			{
				GET_PTR(game_version_id1) = version_id;
				GET_PTR(game_version_id2) = version_id;
				GET_PTR(game_version_id3) = version_id;

				if(and_game_build)
				{
					cstring build_string = NULL;

					if(version_id == Enums::_game_version_id_100)
						build_string = NULL; // TODO
					else if(version_id == Enums::_game_version_id_107)
						build_string = NULL; // TODO
					else if(version_id == Enums::_game_version_id_108)
						build_string = NULL; // TODO
					else if(version_id == Enums::_game_version_id_109)
						build_string = NULL; // TODO

					if(build_string != NULL)
					{
						strcpy(GameState::GameBuildString(), build_string);
						strcpy(GameState::GamespyGameBuildString(), build_string);
					}
				}
			}
		}

		bool ChangeAdvertisedGameVersion(cstring version_str, bool and_game_build)
		{
			bool result = true;
			long_enum version_id = 0;

			if( strstr(version_str, "1.00") )
				version_id = Enums::_game_version_id_100;
			else if( strstr(version_str, "1.07") )
				version_id = Enums::_game_version_id_107;
			else if( strstr(version_str, "1.08") )
				version_id = Enums::_game_version_id_108;
			else if( strstr(version_str, "1.09") )
				version_id = Enums::_game_version_id_109;
			else
				result = false;

			if(result)
				ChangeAdvertisedGameVersionId(version_id, and_game_build);

			return result;
		}
	};
};